using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PaymentService.Domain.Models;

namespace PaymentService.Domain.Services
{
    public class TokenRegisterService : ITokenRegisterService
    {
        private readonly HttpClient _httpClient;

        public TokenRegisterService(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> IsValid(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/users", user);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}