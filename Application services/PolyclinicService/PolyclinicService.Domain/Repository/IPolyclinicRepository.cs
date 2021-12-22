using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolyclinicService.Domain.Models.Polyclinic;

namespace PolyclinicService.Domain.Repository
{
    public interface IPolyclinicRepository
    {
        List<Visit> GetVisits();
        int AddVisit(Visit visit);
        int Delete(string patient, string speciality);
        List<Visit> Get(string patient);
        Visit GetVisitByPatient(string patient, string speciality);
    }
}
