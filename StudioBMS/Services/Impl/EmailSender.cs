using System;
using System.Threading.Tasks;

namespace StudioBMS.Services.Impl
{
    public class EmailSender: IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.FromResult(0);
        }
    }
}
