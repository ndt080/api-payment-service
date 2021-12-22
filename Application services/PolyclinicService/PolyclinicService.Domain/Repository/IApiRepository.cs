using System.Collections.Generic;
using PolyclinicService.Domain.Models.Api;

namespace PolyclinicService.Domain.Repository
{
    public interface IApiRepository
    {
        List<ApiKey> GetKeys();
        void AddKey(ApiKey key);
        void RemoveKey(string key);
        void RemoveKeyById(int id);
        bool CheckAccess(string key);
        bool CheckValid(ApiKey key);
    }
}