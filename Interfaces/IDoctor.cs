using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IDoctor
    {
        ICollection<Doctor> GetDoctors();
        Doctor GetDoctor(int doctorID);
        Doctor GetDoctor(string name);

        ICollection<Doctor> GetDoctorBySpeciality(string  speciality);
        ICollection<Doctor> GetDoctorByDepartment(string departmentName);

        ICollection<Appointment> GetAppointments();
        ICollection<Appointment> GetAppointments(int doctorId);

        bool DoctorExists(int doctorId);
        bool CreateDoctor(int departmentId, Doctor doctor);

        bool updateDoctor(int departmentId, Doctor doctor);
        bool deleteDoctor(int doctorId);
        bool Save();

        bool setAvailability (int doctorId, string availability);
        bool updateAvailability (int doctorId, string availability);
        bool deleteAvailability (int doctorId);



    }
}
