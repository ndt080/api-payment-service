using System.Collections.Generic;
using System.Threading.Tasks;
using PolyclinicService.Domain.Models.Api;

namespace PolyclinicService.Domain.Services.Access
{
    public interface IAccessService
    {
        Task<List<ApiKey>> GetKeysList();
        Task<bool> CheckAccess(string apiKey);
        Task AddApiKey(ApiKey apiKey);
        Task RemoveApiKey(string apiKey);
        Task RemoveApiKeyById(int id);
    }
}