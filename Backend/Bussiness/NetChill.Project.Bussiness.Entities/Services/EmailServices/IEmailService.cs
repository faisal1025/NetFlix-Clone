using System.Threading.Tasks;

namespace NetChill.Project.Bussiness.Entities.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendRecoveryEmail(SendEmailOptions sendEmailOptions);
    }
}