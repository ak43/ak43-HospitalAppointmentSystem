using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using System.Numerics;

namespace HospitalAppointmentSystem.Repositories
{
    public class DoctorAvailabilityRepository : IDoctorAvailabilityRepository
    {
        private readonly DataContext _context;

        public DoctorAvailabilityRepository(DataContext context)  
        {
            _context = context;
        }
        public ICollection<DoctorAvailability> GetAvailabilities()
        {
            return _context.DoctorAvailabilities.OrderBy(a => a.DoctorId).ToList();
        }
        public DoctorAvailability GetAvailability(int Id)
        {
            return _context.DoctorAvailabilities.Where(a => a.Id == Id).FirstOrDefault();
        }

        public ICollection<DoctorAvailability> GetAvailabilityByDoctor(int doctorId)
        {
            return _context.DoctorAvailabilities.Where(a => a.DoctorId == doctorId).ToList();        
        }

        public bool DoctorAvailabilityExists(int availabilityId)
        {
            return _context.DoctorAvailabilities.Any(a => a.Id == availabilityId);
        }

        public bool SaveAvailability(DoctorAvailability doctorAvailability)
        {
            //var doctorPatientntity = _context.Doctors.Where(a => a.Id == doctorId).FirstOrDefault();
            //_context.Add(doctorAvailability);
            //var docPatient = new DoctorPatient
            //{
            //    Doctor = doctorPatientntity,
            //    Patient = Patient
            //};
            //_context.Add(docPatient);

            _context.Add(doctorAvailability);
            return Save();
        }

        public bool UpdateAvailability(DoctorAvailability doctorAvailability)
        {
            _context.Update(doctorAvailability);
            return Save();
        }

        public bool DeleteAvailability(DoctorAvailability doctorAvailability)
        {
            _context.Remove(doctorAvailability);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
