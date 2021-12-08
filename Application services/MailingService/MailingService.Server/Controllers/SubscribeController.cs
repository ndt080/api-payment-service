using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MailingService.Domain.Models.Api;
using MailingService.Domain.Services.Access;
using MailingService.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailingService.Server.Controllers
{
    [ApiController]
    [Route("api/subscribe/[controller]")]
    public class SubscribeController : ControllerBase
    {
        private readonly IAccessService _accessService;

        public SubscribeController(IAccessService accessService)
        {
            _accessService = accessService;
        }

        [HttpPost("AddKey")]
        public async Task<IActionResult> AddKey(ApiKeyRequest key)
        {
            if (key == null)
            {
                return BadRequest();
            }

            try
            {
                await _accessService.AddApiKey(new ApiKey()
                {
                    Key = key.ApiKey,
                    SubscriptionEnd = key.End,
                    SubscriptionStart = key.Start,
                });
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("RemoveKey")]
        public async Task<IActionResult> RemoveKey(string key)
        {
            if (key == null)
            {
                return BadRequest();
            }

            try
            {
                await _accessService.RemoveApiKey(key);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("RemoveKeyById")]
        public async Task<IActionResult> RemoveKeyById(int id)
        {
            try
            {
                await _accessService.RemoveApiKeyById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}