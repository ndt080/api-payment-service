using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailingService.Domain.Models.Mailing;
using MailingService.Domain.Models.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MailingService.Domain.Services.Mailing
{
    public class MailingService : IMailingService
    {
        private readonly MailSettings _mailSettings;

        public MailingService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmail(Email mailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = mailRequest.Subject
            };
            email.To.Add(MailboxAddress.Parse(mailRequest.Addresses[0]));

            var builder = await BuildEmailBodyAsync(mailRequest);
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendEmails(Email mailRequest)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = mailRequest.Subject
            };

            foreach (var address in mailRequest.Addresses)
            {
                email.To.Add(MailboxAddress.Parse(address));
            }

            var builder = await BuildEmailBodyAsync(mailRequest);
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        private static async Task<BodyBuilder> BuildEmailBodyAsync(Email mailRequest)
        {
            if (mailRequest.Attachments == null) return null;

            var builder = new BodyBuilder();
            foreach (var file in mailRequest.Attachments.Where(file => file.Length > 0))
            {
                byte[] fileBytes;
                await using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }

                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            }

            builder.HtmlBody = mailRequest.Body;
            return builder;
        }

        // private readonly MailSettings _mailSettings;
        //
        //     public async Task SendEmailAsync(MailRequest mailRequest)
        //     {
        //         var email = new MimeMessage();
        //         email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        //         email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
        //         email.Subject = mailRequest.Subject;
        //         var builder = new BodyBuilder();
        //         if (mailRequest.Attachments != null)
        //         {
        //             byte[] fileBytes;
        //             foreach (var file in mailRequest.Attachments)
        //             {
        //                 if (file.Length > 0)
        //                 {
        //                     using (var ms = new MemoryStream())
        //                     {
        //                         file.CopyTo(ms);
        //                         fileBytes = ms.ToArray();
        //                     }
        //                     builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        //                 }
        //             }
        //         }
        //         builder.HtmlBody = mailRequest.Body;
        //         email.Body = builder.ToMessageBody();
        //         using var smtp = new SmtpClient();
        //         smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        //         smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        //         await smtp.SendAsync(email);
        //         smtp.Disconnect(true);
        //     }
        //
        //     public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        //     {
        //         string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
        //         StreamReader str = new StreamReader(FilePath);
        //         string MailText = str.ReadToEnd();
        //         str.Close();
        //         MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail);
        //         var email = new MimeMessage();
        //         email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        //         email.To.Add(MailboxAddress.Parse(request.ToEmail));
        //         email.Subject = $"Welcome {request.UserName}";
        //         var builder = new BodyBuilder();
        //         builder.HtmlBody = MailText;
        //         email.Body = builder.ToMessageBody();
        //         using var smtp = new SmtpClient();
        //         smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        //         smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        //         await smtp.SendAsync(email);
        //         smtp.Disconnect(true);
        //     }
    }
}