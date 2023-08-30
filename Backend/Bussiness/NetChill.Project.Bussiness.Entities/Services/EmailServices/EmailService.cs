using Microsoft.Extensions.Options;
using MimeKit;
using NetChill.Project.Bussiness.Entities.UserDomains.AppServices.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace NetChill.Project.Bussiness.Entities.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplates/{0}.html";
        private readonly EmailConf emailConf;
        public EmailService(IOptions<EmailConf> emailConfig)
        {
            emailConf = emailConfig.Value;
        }
        public async Task SendRecoveryEmail(SendEmailOptions sendEmailOptions)
        {
            sendEmailOptions.Subject = "Password Recovery Email";
            string body = getMailBody("ResetPasswordTemplate");
            sendEmailOptions.Body = body;
            await EmailSendService(sendEmailOptions);
        }

        private async Task EmailSendService(SendEmailOptions sendEmailOptions)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(emailConf.SenderAddress));
            mail.Subject = sendEmailOptions.Subject;
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = sendEmailOptions.Body };

            foreach (var to in sendEmailOptions.SendTo) 
            {
                mail.To.Add(MailboxAddress.Parse(to));
            }

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(emailConf.Host, emailConf.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(emailConf.UserName, emailConf.Password);
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);
        }

        private string getMailBody(string templateName)
        {
            string path = string.Format(templatePath, templateName);
            string body = File.ReadAllText(path);
            return body;
        }
    }
}
