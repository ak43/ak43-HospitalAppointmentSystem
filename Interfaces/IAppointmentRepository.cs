using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IAppointmentRepository 
    {
        ICollection<Appointment> GetAppointmentsByDoctor(int doctorId);
        ICollection<Appointment> GetAppointmentsByPatient(int patientId);
        bool IsAppointmentSlotAvailable(int doctorId, DateTime appointmentDateTime);
    } 
} 
