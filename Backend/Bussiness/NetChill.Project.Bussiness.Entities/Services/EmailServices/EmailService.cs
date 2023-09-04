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
using Microsoft.IdentityModel.Tokens;

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
            sendEmailOptions.Subject = UpdatePlaceHolder("Hi, {{UserName}} This is your Password Recovery Email", sendEmailOptions.Placeholder);
            string body = UpdatePlaceHolder(getMailBody("ResetPasswordTemplate"), sendEmailOptions.Placeholder);
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
        private string UpdatePlaceHolder(string text, IList<KeyValuePair<string, string>> values)
        {
            if (!string.IsNullOrEmpty(text) && values != null)
            {
                foreach (var item in values)
                {
                    text = text.Replace(item.Key, item.Value);
                }
            }
            return text;
        }
    }
}
