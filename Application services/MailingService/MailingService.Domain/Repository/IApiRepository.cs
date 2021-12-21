using System.Collections.Generic;
using System.Threading.Tasks;
using MailingService.Domain.Models.Api;

namespace MailingService.Domain.Repository
{
    public interface IApiRepository
    {
        void AddKey(ApiKey key);
        List<ApiKey> GetKeys();
        void RemoveKey(string key);
        void RemoveKeyById(int id);
        bool CheckAccess(string key);
        bool CheckValid(ApiKey key);
    }
}