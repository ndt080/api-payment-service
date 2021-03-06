using System.Threading.Tasks;
using MailingService.Domain.Models.Mailing;

namespace MailingService.Domain.Services.Mailing
{
    public interface IMailingService
    {
        Task SendEmail(Email mailRequest);
        Task SendEmails(Email mailRequest);
    }
}