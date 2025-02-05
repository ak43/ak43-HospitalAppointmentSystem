using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        Patient GetPatient(int patientId);
        ICollection<Patient> GetPatients(string name);

        ICollection<Patient> GetPatientByDepartment(int departmentName);

        // Task<IEnumerable<Appointment>> GetPatientAppointmentsAsync(int patientId);
        ICollection<Appointment> GetPatientAppointments(int patientId);
        bool PatientExists(int PatientId);
        bool SavePatient(int doctorId, Patient Patient);
        //bool CreatePatient(int departmentId, Patient Patient);

        bool UpdatePatient(Patient Patient);
        bool DeletePatient(Patient Patient);
        bool Save();

    }
}
