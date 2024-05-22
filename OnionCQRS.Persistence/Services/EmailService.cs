using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Domain.Model;
using System.Net.Mail;
using System.Net;
using OnionCQRS.Persistence.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace OnionCQRS.Persistence.Services
{
    public class EmailService: IEmailService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public EmailService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(CancellationToken cancellationToken, EmailRequest emailRequest)
        {
            var _emailConfig = _configuration.GetSection("MailSettings").Get<EmailConfig>() ?? new EmailConfig();
            try
            {
                SmtpClient smtpClient = new SmtpClient(_emailConfig.Provider, _emailConfig.Port);
                smtpClient.Credentials = new NetworkCredential(_emailConfig.DefaultSender, _emailConfig.Password);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_emailConfig.DefaultSender);
                mailMessage.To.Add(emailRequest.To);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = emailRequest.Subject;
                mailMessage.Body = emailRequest.Content;

                if (emailRequest.AttachmentFilePaths.Length > 0)
                {
                    foreach (var path in emailRequest.AttachmentFilePaths)
                    {
                        Attachment attachment = new Attachment(path);

                        mailMessage.Attachments.Add(attachment);
                    }
                }

                await smtpClient.SendMailAsync(mailMessage, cancellationToken);

                mailMessage.Dispose();
            }
            catch (Exception ex)
            {
                //log ex
                throw;
            }
        }

        public async Task<string> GetTemplate(string templateName)
        {
            string templateEmail = Path.Combine(_webHostEnvironment.ContentRootPath, templateName);

            string content = await File.ReadAllTextAsync(templateEmail);

            return content;
        }
    }
}
