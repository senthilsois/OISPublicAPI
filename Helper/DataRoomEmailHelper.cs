using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OISPublic.Helper
{
    public class DataRoomEmailHelper
    {

        public static async Task SendFromDataRoomEmailAsync(SendEmailSpecification spec)
        {
            if (spec == null)
                throw new ArgumentNullException(nameof(spec));

            if (string.IsNullOrWhiteSpace(spec.FromAddress))
                throw new ArgumentException("FromAddress is required.");

            if (string.IsNullOrWhiteSpace(spec.ToAddress))
                throw new ArgumentException("ToAddress is required.");

            using var message = new MailMessage
            {
                From = new MailAddress(spec.FromAddress, "Nexa OfficeInfoSystems"),
                Subject = spec.Subject ?? "No Subject",
                Body = spec.Body ?? string.Empty,
                IsBodyHtml = true,
                Priority = spec.Priority switch
                {
                    "High" => MailPriority.High,
                    "Low" => MailPriority.Low,
                    _ => MailPriority.Normal
                }
            };


            foreach (var to in spec.ToAddress.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                message.To.Add(to);


            if (!string.IsNullOrWhiteSpace(spec.CCAddress))
            {
                foreach (var cc in spec.CCAddress.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                    message.CC.Add(cc);
            }


      

            using var smtp = new SmtpClient(spec.Host)
            {
                Port = spec.Port,
                EnableSsl = spec.IsEnableSSL,
                Credentials = new NetworkCredential(spec.FromAddress, spec.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 10000
            };

            try
            {
                await smtp.SendMailAsync(message);
            }
            catch (SmtpException ex)
            {

                throw new InvalidOperationException("Failed to send email.", ex);
            }
            finally
            {

                
                
            }
        }
    }
}
