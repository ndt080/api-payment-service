using System.Threading.Tasks;
using PaymentService.Domain.Models;

namespace PaymentService.Domain.Services
{
    public interface ISubscribeRegisterService
    {
        Task<ServiceInfo> GetServiceInfo(string userService);
        Task<string> GenerateApiKey();

        Task SendApiKeyToService(SubscriptionInfo info, string url, string methodName);
    }
}