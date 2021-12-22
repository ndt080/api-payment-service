using System.Collections.Generic;
using System.Threading.Tasks;
using PolyclinicService.Domain.Models.Polyclinic;

namespace PolyclinicService.Domain.Services.Polyclinic
{
    public interface IPolyclinicService
    {
        Task<List<Visit>> GetVisits();
        Task<int> AddVisit(Visit visit);
        Task<int> Delete(string patient, string speciality);
        Task<List<Visit>> Get(string patient);
        Task<Visit> GetVisitByPatient(string patient, string speciality);
    }
}