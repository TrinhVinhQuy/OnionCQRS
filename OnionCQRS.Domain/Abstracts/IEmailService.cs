using OnionCQRS.Domain.Model;

namespace OnionCQRS.Domain.Abstracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(CancellationToken cancellationToken, EmailRequest emailRequest);
        Task<string> GetTemplate(string templateName);
    }
}
