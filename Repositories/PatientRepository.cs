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
        public bool SavePatient(int doctorId, Patient Patient) 
        {
            var doctorPatientntity = _context.Doctors.Where(a => a.Id == doctorId).FirstOrDefault();
            //var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            _context.Add(Patient);

            var docPatient = new DoctorPatient
            {
                Doctor = doctorPatientntity,
                Patient = Patient
            };
            _context.Add(docPatient);

            
            return Save();
        }

        public bool DeletePatient(Patient Patient)
        {
            _context.Remove(Patient);
            return Save();
        }

        public Patient GetPatient(int patientId)
        {
            return _context.Patients.Where(p => p.Id == patientId).FirstOrDefault();
        }

        public ICollection<Patient> GetPatientByDepartment(int departmentName)
        {
            //return _context.Patients.Where(p => p.de)
            return null;
        }

        public ICollection<Patient> GetPatients()
        {
            return _context.Patients.OrderBy(p => p.FirstName).ToList();
        }

        public ICollection<Patient> GetPatients(string firstName)
        {
            return _context.Patients.Where(p => p.FirstName == firstName).ToList();
        }

        public bool PatientExists(int patientId)
        {
            return _context.Patients.Any(p => p.Id == patientId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePatient(Patient Patient)
        {
            _context.Update(Patient);
            return Save();
        }

        public ICollection<Appointment> GetPatientAppointments(int patientId)
        {
            throw new NotImplementedException();
        }
    }
}
