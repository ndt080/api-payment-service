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

        public Task<bool> CheckAccess(string apiKey)
        {
            return Task.Run(() => _apiRepository.CheckAccess(apiKey));
        }

        public Task AddApiKey(ApiKey apiKey)
        {
            return Task.Run(() => _apiRepository.AddKey(apiKey));
        }

        public Task RemoveApiKey(string apiKey)
        {
            return Task.Run(() => _apiRepository.RemoveKey(apiKey));
        }

        public Task RemoveApiKeyById(int id)
        {
            return Task.Run(() => _apiRepository.RemoveKeyById(id));
        }
    }
}