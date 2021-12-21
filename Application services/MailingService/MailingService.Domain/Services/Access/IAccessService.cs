using System.Collections.Generic;
using System.Threading.Tasks;
using MailingService.Domain.Models.Api;

namespace MailingService.Domain.Services.Access
{
    public interface IAccessService
    {
        Task<bool> CheckAccess(string apiKey);
        Task<List<ApiKey>> GetKeysList();
        Task AddApiKey(ApiKey apiKey);
        Task RemoveApiKey(string apiKey);
        Task RemoveApiKeyById(int id);
    }
}