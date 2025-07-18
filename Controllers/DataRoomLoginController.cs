using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public DataRoomLoginController(
            OISDataRoomContext context,
            IConfiguration configuration,
            JwtTokenService jwtTokenService)
        {
            _context = context;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] EncryptedLoginRequest encryptedRequest)
        {
            if (string.IsNullOrWhiteSpace(encryptedRequest.EncryptedData) ||
                string.IsNullOrWhiteSpace(encryptedRequest.EncryptedKey) ||
                string.IsNullOrWhiteSpace(encryptedRequest.EncryptedIV))
            {
                return BadRequest("Missing encrypted payload.");
            }

            try
            {
         
                string decryptedJson = AesEncryptionHelper.DecryptForPayloadData(
                    encryptedRequest.EncryptedData,
                    encryptedRequest.EncryptedKey,
                    encryptedRequest.EncryptedIV
                );

                var request = JsonConvert.DeserializeObject<OISPublic.OISDataRoomDto.LoginRequest>(decryptedJson);

                if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest(new LoginResponse
                    {
                        Success = false,
                        Message = "Email and password are required.",
                        User = null
                    });
                }

                var user = await _context.DataRoomMasterUsers
                    .FirstOrDefaultAsync(u => u.Email == request.Email &&
                                              u.IsDeleted == false&&
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
                        Message = "Incorrect password.",
                        User = null
                    });
                }

                bool hasActiveDataRoom = await _context.DataRoomUsers
                    .AnyAsync(du => du.UserId == user.Id && du.IsDeleted ==false);

                if (!hasActiveDataRoom)
                {
                    return Unauthorized(new LoginResponse
                    {
                        Success = false,
                        Message = "No active DataRoom found for this user.",
                        User = null
                    });
                }

                bool isFirstLogin = user.IsActive != true;

                if (isFirstLogin)
                {
                    user.IsActive = true;
                    user.CreatedAt ??= DateTime.UtcNow;
                }

                var token = _jwtTokenService.GenerateJwtToken(user);
                user.Token = token;

                _context.DataRoomMasterUsers.Update(user);
                await _context.SaveChangesAsync();

             
                if (isFirstLogin)
                {
                    try
                    {
                        var smtp = await _context.EmailSmtpsettings.FirstOrDefaultAsync(x => !x.IsDeleted && x.ClientId == user.ClientId && x.CompanyId == user.CompanyId);
                        if (smtp != null)
                        {
                            var emailBody = new CustomEmails().GenerateEmailBodyForFirstTimeLogin(user.Email);
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
                    catch (Exception)
                    {
                       
                    }
                }

                var resultPayload = new
                {
                    Success = true,
                    Message = "Login successful.",
                    Token = token,
                    User = new
                    {
                        user.Id,
                        user.Email,
                        user.Name,
                        user.IsExternal,
                        user.ClientId,
                        user.CompanyId
                    }
                };

                string responseJson = JsonConvert.SerializeObject(resultPayload);
                var encryptedResponse = AesEncryptionHelper.EncryptWithRandomKey(responseJson);

                return Ok(encryptedResponse);
            }
            catch (Exception ex)
            {
    
                return StatusCode(500, new LoginResponse
                {
                    Success = false,
                    Message = "An unexpected error occurred during login.",
                    User = null
                });
            }
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest(new { Success = false, Message = "Email is required." });
            }

            var user = await _context.DataRoomMasterUsers
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.IsDeleted ==false);

            if (user == null)
            {
                return NotFound(new { Success = false, Message = "Email not found." });
            }

            var smtp = await _context.EmailSmtpsettings
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.ClientId == user.ClientId && x.CompanyId == user.CompanyId);

            if (smtp == null)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Password reset failed. SMTP settings not configured for your account."
                });
            }

            var newPassword = PasswordGenerator.GenerateRandomPassword(10);

            try
            {
                var emailBody = new CustomEmails().GenerateEmailBodyForNewPassword(user.Email, newPassword);
                var spec = new SendEmailSpecification
                {
                    FromAddress = smtp.UserName,
                    ToAddress = user.Email,
                    Subject = "Nexa Vault DataRoom - Password Reset",
                    Body = emailBody,
                    Host = smtp.Host,
                    Port = smtp.Port,
                    IsEnableSSL = smtp.IsEnableSsl,
                    UserName = smtp.UserName,
                    Password = smtp.Password,
                    Priority = "Normal"
                };

         
                await DataRoomEmailHelper.SendFromDataRoomEmailAsync(spec);

             
                user.Password = newPassword;
                user.CreatedAt = DateTime.UtcNow;

                _context.DataRoomMasterUsers.Update(user);
                await _context.SaveChangesAsync();

                return Ok(new { Success = true, Message = "A new password has been sent to your email." });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Password reset failed while sending email.",
                    Error = ex.Message
                });
            }
        }


    }
}
