using System.Collections.Generic;
using System.Threading.Tasks;
using PolyclinicService.Domain.Models.Polyclinic;
using PolyclinicService.Domain.Repository;

namespace PolyclinicService.Domain.Services.Polyclinic
{
    public class PolyclinicService : IPolyclinicService
    {
        private readonly IPolyclinicRepository _polyclinicRepository;

        public PolyclinicService(IPolyclinicRepository polyclinicRepository)
        {
            _polyclinicRepository = polyclinicRepository;
        }
        public Task<List<Visit>> GetVisits()
        {
            return Task.Run(() => _polyclinicRepository.GetVisits());
        }
        public Task<List<Visit>> Get(string patient)
        {
            //await using var db = new VisitContext();
            return  Task.Run(() => _polyclinicRepository.Get(patient));
        }
        public Task<int> AddVisit(Visit visit)
        {
            //await using var db = new VisitContext();
            return Task.Run(() => _polyclinicRepository.AddVisit(visit));
        }
        public Task<int> Delete(string patient, string speciality)
        {
            return Task.Run(() => _polyclinicRepository.Delete(patient, speciality));
        }

        public Task<Visit> GetVisitByPatient(string patient, string speciality)
        {
            return Task.Run(() => _polyclinicRepository.GetVisitByPatient(patient, speciality));
        }
    }
}