using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISubscribeRegisterService _registerService;

        public SubscribeController(IUserService userService, ISubscribeRegisterService registerService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _registerService = registerService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubscriptionRequest request)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var res = await _registerService.GetServiceInfo(request.ServiceName);
            if (res is null)
                return BadRequest(new { message = "Invalid Applied Service name" });
            
            if (request.PaymentAmount < res.SubscriptionCost)
                return BadRequest(new { message = "insufficient funds for subscription." });

            var apiKey = await _registerService.GenerateApiKey();
            var subscription = new SubscriptionInfo
            {
                ServiceName = res.Name,
                ApiKey = apiKey,
                Start = DateTime.Now,
                End = DateTime.Now.AddDays(res.SubscriptionDuration)
            };
            
            // TODO: Сохранить подписку в юзера (update).
            await _registerService.SendApiKeyToService(subscription, res.Url, res.AddKeyMethod);
            return Ok(subscription);
        }

    }
}