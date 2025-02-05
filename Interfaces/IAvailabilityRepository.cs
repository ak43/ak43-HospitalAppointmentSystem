using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IAvailabilityRepository
    {
        public ICollection<Availability> GetAvailabilities();
        public Availability GetAvailability(int Id);
        public ICollection<Availability> GetAvailabilityByDoctor(int doctorId);
        public bool DoctorAvailabilityExists(int availabilityId);
        public bool SaveAvailability(Availability doctorAvailability);
        public bool UpdateAvailability(Availability doctorAvailability);
        public bool DeleteAvailability(Availability doctorAvailability);
        public bool Save();
    }
}
