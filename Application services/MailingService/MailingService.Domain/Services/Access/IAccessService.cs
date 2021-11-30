using System.Threading.Tasks;
using MailingService.Domain.Models.Api;

namespace MailingService.Domain.Services.Access
{
    public interface IAccessService
    {
        Task<bool> CheckAccess(string apiKey);
        Task<bool> AddApiKey(ApiKey apiKey);
        Task<bool> RemoveApiKey(ApiKey apiKey);
        Task<bool> RemoveApiKeyById(string id);
    }
}