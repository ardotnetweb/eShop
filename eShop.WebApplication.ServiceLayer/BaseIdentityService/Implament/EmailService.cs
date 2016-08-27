using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eShop.WebApplication.ServiceLayer.BaseIdentityService.Implament
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            configSendGridasync(message);
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
        private Task configSendGridasync(IdentityMessage message)
        {
            SmtpClient SmtpServer = new SmtpClient("mail.inspectelement.us");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("mail@inspectelement.us");
            mail.To.Add(message.Destination);
            mail.Subject = message.Subject;
            mail.Body +=message.Body;
            mail.IsBodyHtml = true;

            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            SmtpServer.Host = "mail.inspectelement.us";
            //SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.
                NetworkCredential("mail@inspectelement.us", "Ar2480014037@");
            //SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            return Task.FromResult(0);
        }
    }
}
