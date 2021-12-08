using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Domain.AuthUtils;
using PaymentService.Domain.IRepositories;
using PaymentService.Domain.Models;
using PaymentService.Domain.Services;
using PaymentService.Server.Models;

namespace PaymentService.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubscribeController : ControllerBase
    {
        private readonly ISubscribeRegisterService _registerService;
        private readonly IUserRepository _userRepository;

        public SubscribeController(ISubscribeRegisterService registerService, IUserRepository userRepository)
        {
            _registerService = registerService;
            _userRepository = userRepository;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubscriptionRequest request)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var res = await _registerService.GetServiceInfo(request.ServiceName);
            if (res is null)
                return BadRequest(new { message = "Invalid Applied Service name" });

            if (request.PaymentAmount < res.SubscriptionCost)
                return BadRequest(new { message = "Insufficient funds for subscription." });

            var apiKey = await _registerService.GenerateApiKey();
            var subscription = new SubscriptionInfo
            {
                Id = request.Id,
                ServiceName = res.Name,
                ApiKey = apiKey,
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(res.SubscriptionDuration)
            };

            _userRepository.AddSubscriptions(subscription);

            await _registerService.SendApiKeyToService(subscription, res.Url, res.AddKeyMethod);
            return Ok(subscription);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Test()
        {
            var t = new SubscriptionInfo
            {
                Id = 2,
                ServiceName = "test",
                ApiKey = "key-to-delete",
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(10)
            };
            
            _userRepository.AddSubscriptions(t);

            return Ok(t);
        }
    }
}