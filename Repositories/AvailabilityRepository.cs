using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ICollection<Availability>> GetAvailabilities()
        {
            // return _context.Availability.OrderBy(a => a.DoctorId).ToList();
            return await _context.Availability.ToListAsync();
        }

        public async Task<Availability> GetAvailability(int Id)
        {
            return await _context.Availability.Where(a => a.Id == Id).FirstOrDefaultAsync();
            //return availability;
        }

        public async Task<ICollection<Availability>> GetAvailabilityByDoctor(int doctorId)
        {
            return await _context.Availability.Where(a => a.DoctorId == doctorId).ToListAsync();        
        }

        public async Task<bool> DoctorAvailabilityExists(int availabilityId)
        {
            return await _context.Availability.AnyAsync(a => a.Id == availabilityId);
        }

        public async Task<bool> SaveAvailability(Availability doctorAvailability)
        {
            //var doctorPatientntity = _context.Doctors.Where(a => a.Id == doctorId).FirstOrDefault();
            //_context.Add(doctorAvailability);
            //var docPatient = new DoctorPatient
            //{
            //    Doctor = doctorPatientntity,
            //    Patient = Patient
            //};
            //_context.Add(docPatient);

            await _context.AddAsync(doctorAvailability);
            return await Save();
        }

        public async Task<bool> UpdateAvailability(Availability doctorAvailability)
        {
             _context.Update(doctorAvailability);
            return await Save();
        } 

        public async Task<bool> DeleteAvailability(Availability doctorAvailability)
        {
            _context.Remove(doctorAvailability);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

       
    }
}
