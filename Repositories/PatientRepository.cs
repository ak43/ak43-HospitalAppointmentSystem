using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DataContext _context;
        public PatientRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> SavePatient(int doctorId, Patient Patient) 
        {
            var doctorPatientntity = await _context.Doctors.Where(a => a.Id == doctorId).FirstOrDefaultAsync();
            //var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            await _context.AddAsync(Patient);

            var docPatient = new DoctorPatient
            {
                Doctor = doctorPatientntity,
                Patient = Patient
            };
            await _context.AddAsync(docPatient);

            
            return await Save();
        }

        public async Task<bool> DeletePatient(Patient Patient)
        {
            _context.Remove(Patient);
            return await Save();
        }

        public async Task<Patient> GetPatient(int patientId)
        {
            return await _context.Patients.Where(p => p.Id == patientId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Patient>> GetPatientByDepartment(int departmentName)
        {
            //return _context.Patients.Where(p => p.de)
            return null;
        }

        public async Task<ICollection<Patient>> GetPatients()
        {
            return await _context.Patients.OrderBy(p => p.FirstName).ToListAsync();
        }

        public async Task<ICollection<Patient>> GetPatients(string firstName)
        {
            return await _context.Patients.Where(p => p.FirstName == firstName).ToListAsync();
        }

        public async Task<bool> PatientExists(int patientId)
        {
            return await _context.Patients.AnyAsync(p => p.Id == patientId);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdatePatient(Patient Patient)
        {
            _context.Update(Patient);
            return await Save();
        }

        public Task<ICollection<Appointment>> GetPatientAppointments(int patientId)
        {
            throw new NotImplementedException();
        }
    }
}
