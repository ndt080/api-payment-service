using System.Threading.Tasks;
using MailingService.Domain.Models.Api;

namespace MailingService.Domain.Services.Access
{
    public class AccessService : IAccessService
    {
        public async Task<bool> CheckAccess(string apiKey)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddApiKey(ApiKey apiKey)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveApiKey(ApiKey apiKey)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveApiKeyById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}