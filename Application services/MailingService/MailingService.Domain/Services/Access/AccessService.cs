using System.Threading.Tasks;
using MailingService.Domain.Models.Api;
using MailingService.Domain.Repository;

namespace MailingService.Domain.Services.Access
{
    public class AccessService : IAccessService
    {
        private readonly IApiRepository _apiRepository;

        public AccessService(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public async Task<bool> CheckAccess(string apiKey)
        {
           return _apiRepository.CheckAccess(apiKey);
        }

        public async Task AddApiKey(ApiKey apiKey)
        {
            _apiRepository.AddKey(apiKey);
        }

        public async Task RemoveApiKey(string apiKey)
        {
            _apiRepository.RemoveKey(apiKey);
        }

        public async Task RemoveApiKeyById(int id)
        {
            _apiRepository.RemoveKeyById(id);
        }
    }
}