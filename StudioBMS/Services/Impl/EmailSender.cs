using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace StudioBMS.Services.Impl
{
    public class EmailSender: IEmailSender
    {
        public static string SERVER_EMAIL = "stepanov-valentin@ukr.net";
        public static string SERVER_EMAIL_PASSWORD = "dfkbrqwe181994";
        public static string SMTP = "smtp.ukr.net";
        public static int SMTP_PORT = 465;

        public EmailSender()
        {
            
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(SERVER_EMAIL));
            emailMessage.To.Add(new MailboxAddress(email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<h1>StudioBMS</h1><p>{message}"
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(SMTP, SMTP_PORT);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(SERVER_EMAIL, SERVER_EMAIL_PASSWORD);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
