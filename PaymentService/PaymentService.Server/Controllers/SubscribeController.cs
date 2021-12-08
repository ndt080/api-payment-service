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
        private readonly IJwtUtils _jwtUtils;

        public SubscribeController(ISubscribeRegisterService registerService, IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _registerService = registerService;
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubscriptionRequest request)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var userId = _jwtUtils.ValidateJwtToken(token) ?? 0;
            
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

            _userRepository.AddSubscriptions(userId, subscription);

            await _registerService.SendApiKeyToService(subscription, res.Url, res.AddKeyMethod);
            return Ok(subscription);
        }
        
        [HttpPost("unsubscribe")]
        public IActionResult Unsubscribe(int subscribeId)
        {
            Request.Headers.TryGetValue("Authorization", out var token);
            var userId = _jwtUtils.ValidateJwtToken(token);
            
            if (userId is null)
                return BadRequest(new { message = "Invalid User" });
            
            _userRepository.DeleteSubscription(userId.Value, subscribeId);
            
            return Ok();
        }
    }
}