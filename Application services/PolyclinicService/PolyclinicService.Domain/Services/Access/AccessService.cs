using System.Collections.Generic;
using System.Threading.Tasks;
using PolyclinicService.Domain.Models.Api;
using PolyclinicService.Domain.Repository;

namespace PolyclinicService.Domain.Services.Access
{
    public class AccessService : IAccessService
    {
        private readonly IApiRepository _apiRepository;

        public AccessService(IApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public Task<List<ApiKey>> GetKeysList()
        {
            return Task.Run(() => _apiRepository.GetKeys());
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