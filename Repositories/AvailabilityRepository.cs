using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using System.Numerics;

namespace HospitalAppointmentSystem.Repositories
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly DataContext _context;

        public AvailabilityRepository(DataContext context)  
        {
            _context = context;
        }
        public ICollection<Availability> GetAvailabilities()
        {
            return _context.Availability.OrderBy(a => a.DoctorId).ToList();
        }
        public Availability GetAvailability(int Id)
        {
            return _context.Availability.Where(a => a.Id == Id).FirstOrDefault();
        }

        public ICollection<Availability> GetAvailabilityByDoctor(int doctorId)
        {
            return _context.Availability.Where(a => a.DoctorId == doctorId).ToList();        
        }

        public bool DoctorAvailabilityExists(int availabilityId)
        {
            return _context.Availability.Any(a => a.Id == availabilityId);
        }

        public bool SaveAvailability(Availability doctorAvailability)
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

        public bool UpdateAvailability(Availability doctorAvailability)
        {
            _context.Update(doctorAvailability);
            return Save();
        } 

        public bool DeleteAvailability(Availability doctorAvailability)
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
