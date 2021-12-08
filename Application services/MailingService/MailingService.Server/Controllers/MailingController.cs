using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailingService.Domain.Models.Mailing;
using MailingService.Domain.Services.Access;
using MailingService.Domain.Services.Mailing;
using MailingService.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailingService.Server.Controllers
{
    [Route("api/mailing/[controller]")]
    [ApiController]
    public class MailingController : ControllerBase
    {
        private readonly IMailingService _mailingService;
        private readonly IAccessService _accessService;

        public MailingController(IMailingService mailingService, IAccessService accessService)
        {
            _mailingService = mailingService;
            _accessService = accessService;
        }

        [HttpPost]
        [Route("SendEmail")]
        public async Task<IActionResult> SendEmail(OneMailingRequest request, string accessKey)
        {
            if (!await _accessService.CheckAccess(accessKey)) return Forbid();

            if (request == null) return BadRequest();

            try
            {
                await _mailingService.SendEmail(new Email
                {
                    Addresses = new List<string> { request.ToEmail },
                    Subject = request.Subject,
                    Body = request.Body,
                    Attachments = request.Attachments
                });
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        [Route("SendToMultipleEmail")]
        public async Task<IActionResult> SendToMultipleEmail(MultipleMailingRequest request, string accessKey)
        {
            if (!await _accessService.CheckAccess(accessKey)) return Forbid();

            if (request == null) return BadRequest();

            try
            {
                await _mailingService.SendEmails(new Email
                {
                    Addresses = request.ToEmails,
                    Subject = request.Subject,
                    Body = request.Body,
                    Attachments = request.Attachments
                });
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}