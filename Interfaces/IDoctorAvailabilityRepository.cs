using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IDoctorAvailabilityRepository
    {
        public ICollection<DoctorAvailability> GetAvailabilities();
        public DoctorAvailability GetAvailability(int Id);
        public ICollection<DoctorAvailability> GetAvailabilityByDoctor(int doctorId);
        public bool DoctorAvailabilityExists(int availabilityId);
        public bool SaveAvailability(DoctorAvailability doctorAvailability);
        public bool UpdateAvailability(DoctorAvailability doctorAvailability);
        public bool DeleteAvailability(DoctorAvailability doctorAvailability);
        public bool Save();
    }
}
