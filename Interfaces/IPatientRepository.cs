using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IPatientRepository
    {
        Task<ICollection<Patient>> GetPatients();
        Task<Patient> GetPatient(int patientId);
        Task<ICollection<Patient>> GetPatients(string name);

        Task<ICollection<Patient>> GetPatientByDepartment(int departmentName);

        // Task<IEnumerable<Appointment>> GetPatientAppointmentsAsync(int patientId);
        Task<ICollection<Appointment>> GetPatientAppointments(int patientId);
        Task<bool> PatientExists(int PatientId);
        Task<bool> SavePatient(int doctorId, Patient Patient);
        //bool CreatePatient(int departmentId, Patient Patient);

        Task<bool> UpdatePatient(Patient Patient);
        Task<bool> DeletePatient(Patient Patient);
        Task<bool> Save();

    }
}
