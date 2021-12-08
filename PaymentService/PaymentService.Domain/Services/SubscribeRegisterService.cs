using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PaymentService.Domain.AuthUtils;
using PaymentService.Domain.Models;

namespace PaymentService.Domain.Services
{
    public class SubscribeRegisterService : ISubscribeRegisterService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _httpClient;


        public SubscribeRegisterService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceInfo> GetServiceInfo(string serviceName)
        {
            _httpClient.BaseAddress = new Uri(_appSettings.RegisterUrl);
            var response = await _httpClient.GetAsync($"/api/serviceInfo?serviceName={serviceName}");
            return await response.Content.ReadFromJsonAsync<ServiceInfo>();
        }

        public async Task SendApiKeyToService(SubscriptionInfo info, string url, string methodName)
        {
            _httpClient.BaseAddress = new Uri(url);
            await _httpClient.PostAsJsonAsync($"/{methodName}", info);
        }

        public async Task<string> GenerateApiKey()
        {
            return await Task.Run(() =>
            {
                using HashAlgorithm algorithm = MD5.Create();

                var hashString = new StringBuilder();
                var inputString = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

                foreach (var h in hash) hashString.Append(h.ToString("x2"));

                return hashString.ToString();
            });
        }
    }
}