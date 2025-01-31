using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IDoctorRepository
    {
        ICollection<Doctor> GetDoctors();
        Doctor GetDoctor(int doctorID);
        ICollection<Doctor> GetDoctors(string name);

        ICollection<Doctor> GetDoctorBySpeciality(string  speciality);
        ICollection<Doctor> GetDoctorByDepartment(int departmentName);

       

        bool DoctorExists(int doctorId);
        bool CreateDoctor(Doctor doctor);
        //bool CreateDoctor(int departmentId, Doctor doctor);

        bool UpdateDoctor(Doctor doctor);
        bool DeleteDoctor(Doctor doctor);
        bool Save();

        ICollection<Appointment> GetAppointments();
        ICollection<Appointment> GetAppointments(int doctorId);
        //bool setAvailability (int doctorId, string availability);
        //bool updateAvailability (int doctorId, string availability);
        //bool deleteAvailability (int doctorId);



    }
}
