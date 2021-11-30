using System;
using System.Threading.Tasks;
using MailingService.Domain.Models.Api;
using MailingService.Domain.Services.Access;
using Microsoft.AspNetCore.Mvc;

namespace MailingService.Server.Controllers {
    [Route("api/subscribe/[controller]")]
    [ApiController]
    public class SubscribeController: ControllerBase {
        private readonly IAccessService _accessService;

        public SubscribeController(IAccessService accessService) {
            _accessService = accessService;
        }
        
        [HttpGet]
        [Route("AddKey")]
        public async Task<IActionResult> AddKey(ApiKey key) {
            if (key == null) {
                return BadRequest();
            }
            
            try {
                await _accessService.AddApiKey(key);
                return Ok();
            } catch (Exception e) {
                return BadRequest(e);
            }
        }
        
        [HttpGet]
        [Route("RemoveKey")]
        public async Task<IActionResult> RemoveKey(ApiKey key) {
            if (key == null) {
                return BadRequest();
            }
            
            try {
                await _accessService.RemoveApiKey(key);
                return Ok();
            } catch (Exception e) {
                return BadRequest(e);
            }
        }
        
        [HttpGet]
        [Route("RemoveKeyById")]
        public async Task<IActionResult> RemoveKeyById(string id) {
            if (id == null) {
                return BadRequest();
            }
            
            try {
                await _accessService.RemoveApiKeyById(id);
                return Ok();
            } catch (Exception e) {
                return BadRequest(e);
            }
        }
    }
}
