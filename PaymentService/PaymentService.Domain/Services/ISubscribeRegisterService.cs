using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using PaymentService.Domain.Models;

namespace PaymentService.Domain.Services
{
    public interface ISubscribeRegisterService
    {
        Task<ServiceInfo> GetServiceInfo(StringValues token, string userService);
        Task<string> GenerateApiKey();

        Task SendApiKeyToService(StringValues token,  SubscriptionInfo info, string url, string methodName);
    }
}