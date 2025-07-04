using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models.ExternalConnectors;
using OISPublic.Helper;
using OISPublic.OISDataRoom;
using OISPublic.OISDataRoomDto;
using OISPublic.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OISPublic.OISDataRoom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataRoomLoginController : ControllerBase
    {
        private readonly OISDataRoomContext _context;

        private readonly JwtTokenService _jwtTokenService;

        private readonly IConfiguration _configuration;


        public DataRoomLoginController(OISDataRoomContext context,
            IConfiguration configuration,
            JwtTokenService jwtTokenService)
        {
            _context = context;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "Email and Password are required.",
                    User = null
                });
            }

            try
            {
                var user = await _context.DataRoomMasterUsers
                    .FirstOrDefaultAsync(u => u.Email == request.Email &&
                                              u.IsDeleted == false &&
                                              u.IsExternal == request.IsExternal);

                if (user == null)
                {
                    return Unauthorized(new LoginResponse
                    {
                        Success = false,
                        Message = "Invalid email or user not registered.",
                        User = null
                    });
                }

                if (user.Password != request.Password)
                {
                    return Unauthorized(new LoginResponse
                    {
                        Success = false,
                        Message = "Password is incorrect.",
                        User = null
                    });
                }



                bool hasActiveDataRoom = await _context.DataRoomUsers
    .AnyAsync(du => du.UserId == user.Id && du.IsDeleted == false);

                if (!hasActiveDataRoom)
                {
                    return Unauthorized(new LoginResponse
                    {
                        Success = false,
                        Message = "No active DataRoom found for this user. Access denied.",
                        User = null
                    });
                }

                bool isFirstLogin = user.IsActive != true;

                if (isFirstLogin)
                {
                    user.IsActive = true;
                    user.CreatedAt = user.CreatedAt ?? DateTime.UtcNow;
                }

                var token = _jwtTokenService.GenerateJwtToken(user);
                user.Token = token;
           
                _context.DataRoomMasterUsers.Update(user);
                await _context.SaveChangesAsync();

                if (isFirstLogin)
                {
                    var emailHelper = new CustomEmails();
                    var emailBody = emailHelper.GenerateEmailBodyForFirstTimeLogin(user.Email);



                    var smtp = await _context.EmailSmtpsettings
    .FirstOrDefaultAsync(x => x.IsDeleted == true);

                    if (smtp != null)
                    {
                        var spec = new SendEmailSpecification
                        {
                            FromAddress = smtp.UserName,
                            ToAddress = user.Email,
                            Subject = "Welcome to Nexa Vault DataRoom",
                            Body = emailBody,
                            Host = smtp.Host,
                            Port = smtp.Port,
                            IsEnableSSL = smtp.IsEnableSsl,
                            UserName = smtp.UserName,
                            Password = smtp.Password,
                            Priority = "Normal"
                        };

                        await DataRoomEmailHelper.SendFromDataRoomEmailAsync(spec);
                    }

                }





                return Ok(new
                {
                    Success = true,
                    Message = "Login successful.",
                    Token = token,
                    User = user
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponse
                {
                    Success = false,
                    Message = $"An error occurred while processing your request: {ex.Message}",
                    User = null
                });
            }
        }


    }
}
