using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IAvailabilityRepository
    {
        public Task<ICollection<Availability>> GetAvailabilities();
        public Task<Availability> GetAvailability(int Id);
        public Task<ICollection<Availability>> GetAvailabilityByDoctor(int doctorId);
        public Task<bool> DoctorAvailabilityExists(int availabilityId);
        public Task<bool> SaveAvailability(Availability doctorAvailability);
        public Task<bool> UpdateAvailability(Availability doctorAvailability);
        public Task<bool> DeleteAvailability(Availability doctorAvailability);
        public Task<bool> Save();
    }
}
