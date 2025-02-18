using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IDoctorRepository
    {
        Task<ICollection<Doctor>> GetDoctors();
        Task<Doctor> GetDoctor(int doctorID);
        Task<ICollection<Doctor>> GetDoctors(string name);

        Task<ICollection<Doctor>> GetDoctorBySpeciality(string  speciality);
        Task<ICollection<Doctor>> GetDoctorByDepartment(int departmentName);

        // Get Doctor's Available dates and times
        Task<ICollection<Availability>> GetDoctorAvailability(int doctorId);

        Task<bool> DoctorExists(int doctorId);
        Task<bool> CreateDoctor(Doctor doctor);
        //bool CreateDoctor(int departmentId, Doctor doctor);

        Task<bool> UpdateDoctor(Doctor doctor);
        Task<bool> DeleteDoctor(Doctor doctor);
        Task<bool> Save();

        Task<ICollection<Appointment>> GetAppointments();
        Task<ICollection<Appointment>> GetAppointments(int doctorId);
        //bool setAvailability (int doctorId, string availability);
        //bool updateAvailability (int doctorId, string availability);
        //bool deleteAvailability (int doctorId);
    }
}
