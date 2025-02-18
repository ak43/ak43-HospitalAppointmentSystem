using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _context;

        public DoctorRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Doctor>> GetDoctors()
        {
            return await _context.Doctors.OrderBy(d => d.FirstName).ToListAsync();
        }

        public async Task<Doctor> GetDoctor(int doctorID)
        {
            return await _context.Doctors.Where(d => d.Id == doctorID).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Doctor>> GetDoctors(string firstName)
        {
            return await _context.Doctors.Where(d => d.FirstName.Contains(firstName)).ToListAsync();
        }

        public async Task<ICollection<Doctor>> GetDoctorByDepartment(int departmentId)
        {
            return await _context.Doctors.Where(d => d.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<ICollection<Doctor>> GetDoctorBySpeciality(string speciality)
        {
            return await _context.Doctors.Where(d => d.Specialization == speciality).ToListAsync();
        }

        public async Task<bool> CreateDoctor(Doctor doctor)
        {

           await _context.AddAsync(doctor);
            return await Save();
        }


        public async Task<bool> UpdateDoctor(Doctor doctor)
        {
             _context.Update(doctor);
            return await Save();
        }

        public async Task<bool> DeleteDoctor(Doctor doctor)
        {
            _context.Remove(doctor);
            return await Save();
        }

        public async Task<bool> DoctorExists(int doctorId)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == doctorId);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public async Task<ICollection<Appointment>> GetAppointments()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Appointment>> GetAppointments(int doctorId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetAvailability(int doctorId, string availability)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAvailability(int doctorId, string availability)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteAvailability(int doctorId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Availability>> GetDoctorAvailability(int doctorId)
        {
            throw new NotImplementedException();
        }
    }
}
