using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolyclinicService.Database.Context;
using PolyclinicService.Domain.Models.Polyclinic;
using PolyclinicService.Domain.Repository;

namespace PolyclinicService.Database.Repository
{
    public class PolyclinicRepository : IPolyclinicRepository
    {
        private VisitContext db;

        public PolyclinicRepository(VisitContext context)
        {
            db = context;
        }
        public List<Visit> GetVisits()
        {
            //await using var db = new VisitContext();
            return db.Visits.ToList();
        }
        public List<Visit> Get(string patient)
        {
            //await using var db = new VisitContext();
            return db.Visits.Select(visit => visit).Where(visit => visit.PatientFio == patient).ToList();
        }
        public int AddVisit(Visit visit)
        {
            //await using var db = new VisitContext();
            db.Visits.Add(visit);
            return db.SaveChanges();
        }
        public int Delete(string patient, string speciality)
        {
            //await using var db = new VisitContext();
            var visit = GetVisitByPatient(patient, speciality);
            db.Visits.Remove(visit);
            return db.SaveChanges();
        }

        public Visit GetVisitByPatient(string patient, string speciality)
        {
            //await using var db = new VisitContext();
            return db.Visits.FirstOrDefault(x => x.PatientFio == patient && x.Speciality == speciality);
        }
    }
}
