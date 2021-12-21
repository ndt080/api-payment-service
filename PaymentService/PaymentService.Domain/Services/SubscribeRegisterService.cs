using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using PaymentService.Domain.AuthUtils;
using PaymentService.Domain.Models;

namespace PaymentService.Domain.Services
{
    public class SubscribeRegisterService : ISubscribeRegisterService
    {
        private readonly AppSettings _appSettings;

        public SubscribeRegisterService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceInfo> GetServiceInfo(StringValues token, string serviceName)
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(_appSettings.RegisterUrl),
                DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", token) }
            };
            var response = await httpClient.GetAsync($"/getServiceInfo?name={serviceName}");
            return await response.Content.ReadFromJsonAsync<ServiceInfo>();
        }

        public async Task SendApiKeyToService(StringValues token, SubscriptionInfo info, string url, string methodName)
        {
            using var httpClient = new HttpClient();
            var payload = JsonConvert.SerializeObject(info);
            await httpClient.PostAsync($"{url}/{methodName}",
                new StringContent(payload, Encoding.UTF8, "application/json"));
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