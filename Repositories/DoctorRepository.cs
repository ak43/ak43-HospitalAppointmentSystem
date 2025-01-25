using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Repositories
{
    public class DoctorRepository : IDoctor
    {
        public bool CreateDoctor(int departmentId, Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public bool deleteAvailability(int doctorId)
        {
            throw new NotImplementedException();
        }

        public bool deleteDoctor(int doctorId)
        {
            throw new NotImplementedException();
        }

        public bool DoctorExists(int doctorId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Appointment> GetAppointments()
        {
            throw new NotImplementedException();
        }

        public ICollection<Appointment> GetAppointments(int doctorId)
        {
            throw new NotImplementedException();
        }

        public Doctor GetDoctor(int doctorID)
        {
            throw new NotImplementedException();
        }

        public Doctor GetDoctor(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<Doctor> GetDoctorByDepartment(string departmentName)
        {
            throw new NotImplementedException();
        }

        public ICollection<Doctor> GetDoctorBySpeciality(string speciality)
        {
            throw new NotImplementedException();
        }

        public ICollection<Doctor> GetDoctors()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool setAvailability(int doctorId, string availability)
        {
            throw new NotImplementedException();
        }

        public bool updateAvailability(int doctorId, string availability)
        {
            throw new NotImplementedException();
        }

        public bool updateDoctor(int departmentId, Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
