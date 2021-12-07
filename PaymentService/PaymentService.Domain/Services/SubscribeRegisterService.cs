using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Threading.Tasks;
using PaymentService.Domain.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;


namespace PaymentService.Domain.Services
{
    public class SubscribeRegisterService : ISubscribeRegisterService
    {
        private readonly HttpClient _httpClient;

        public SubscribeRegisterService()
        {
        }

        public SubscribeRegisterService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<ServiceInfo> GetServiceInfo(string serviceName)
        {
            var response = await _httpClient.GetAsync($"/api/serviceInfo?serviceName={serviceName}");
            return await response.Content.ReadFromJsonAsync<ServiceInfo>();
        }

        public async Task SendApiKeyToService(SubscriptionInfo info, string url, string methodName)
        {
            await _httpClient.PostAsJsonAsync($"{url}/{methodName}", info);
        }

        public async Task<string> GenerateApiKey()
        {
            return await Task.Run(() =>
            {
                using HashAlgorithm algorithm = MD5.Create();

                var hashString = new StringBuilder();
                var inputString = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

                foreach (var h in hash)
                {
                    hashString.Append(h.ToString("x2"));
                }

                return hashString.ToString();
            });
        }
    }
}