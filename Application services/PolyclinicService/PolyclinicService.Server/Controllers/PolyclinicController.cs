using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PolyclinicService.Domain.Models.Polyclinic;
using PolyclinicService.Domain.Services.Access;
using PolyclinicService.Domain.Services.Polyclinic;

namespace PolyclinicService.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolyclinicController : ControllerBase
    {
        private readonly IPolyclinicService _polyclinicService;
        private readonly IAccessService _accessService;

        public PolyclinicController(IPolyclinicService polyclinicService, IAccessService accessService)
        {
            _polyclinicService = polyclinicService;
            _accessService = accessService;
        }

        
        // GET: api/<ValuesController>
        [HttpGet("GetAllVisits")]
        public async Task<ActionResult<IEnumerable<Visit>>> GetAllVisits(string accessKey)
        {
            if (!await _accessService.CheckAccess(accessKey)) return Forbid();
            return Ok(await _polyclinicService.GetVisits());
        }

        [HttpGet("GetAllPatientVisits")]
        public async Task<ActionResult<IEnumerable<Visit>>> GetAllPatientVisits(string patient, string accessKey)
        {
            if (!await _accessService.CheckAccess(accessKey)) return Forbid();
            return Ok(await _polyclinicService.Get(patient));
        }


        // POST api/<ValuesController>
        [HttpPost("AddVisit")]
        public async Task<ActionResult<Visit>> AddVisit(Domain.Models.Polyclinic.Visit visit, string accessKey)
        {
            if (!await _accessService.CheckAccess(accessKey)) return Forbid();
            if (visit == null)
            {
                return BadRequest();
            }

            await _polyclinicService.AddVisit(visit);
            return Ok(visit);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("DeleteVisit")]
        public async Task<ActionResult<Visit>> DeleteVisit(string patient, string speciality, string accessKey)
        {
            if (!await _accessService.CheckAccess(accessKey)) return Forbid();
            var visit = GetVisitByPatient(patient, speciality);
            if (visit == null)
            {
                return NotFound();
            }

            await _polyclinicService.Delete(patient, speciality);
            return Ok(visit);
        }

        private async Task<Visit> GetVisitByPatient(string patient, string speciality)
        {
            return await _polyclinicService.GetVisitByPatient(patient, speciality);
        }
    }
}