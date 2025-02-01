using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        Patient GetPatient(int PatientID);
        ICollection<Patient> GetPatients(string name);

        ICollection<Patient> GetPatientByDepartment(int departmentName);

        bool PatientExists(int PatientId);
        bool SavePatient(int doctorId, Patient Patient);
        //bool CreatePatient(int departmentId, Patient Patient);

        bool UpdatePatient(Patient Patient);
        bool DeletePatient(Patient Patient);
        bool Save();

    }
}
