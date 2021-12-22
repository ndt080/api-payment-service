using System;
using System.ComponentModel.DataAnnotations;

namespace PolyclinicService.Domain.Models.Polyclinic
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }
        public string DoctorFio { get; set; }
        public string PatientFio { get; set; }
        public DateTime Date { get; set; }
        public string Speciality { get; set; }
    }
}