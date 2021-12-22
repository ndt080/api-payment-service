using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PolyclinicService.Domain.Models.Api;
using PolyclinicService.Domain.Services.Access;
using PolyclinicService.Server.Models;

namespace PolyclinicService.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscribeController : ControllerBase
    {
        private readonly IAccessService _accessService;

        public SubscribeController(IAccessService accessService)
        {
            _accessService = accessService;
        }
        
        [HttpPost("GetKeysList")]
        public async Task<IActionResult> GetKeysList()
        {
            try
            {
                var keys = await _accessService.GetKeysList();
                return Ok(keys);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
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

        [HttpDelete("RemoveKey")]
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

        [HttpDelete("RemoveKeyById")]
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