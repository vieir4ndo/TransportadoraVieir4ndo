using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Buffers;
using TV.SER.Interfaces;
using TV.SER.DTOs;

namespace TV.SER
{
    public class MailJet : IEmail
    {
        public async Task Send(string emailAdress, string body, EmailOptionsDTO options)
        {
            var client = new SmtpClient();
            client.Host = options.Host;
            client.Credentials = new NetworkCredential(options.ApiKey, options.ApiKeySecret);
            client.Port = options.Port;

            var message = new MailMessage(options.SenderEmail, emailAdress);
            message.Body = body;
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);
        }
        
    }
}