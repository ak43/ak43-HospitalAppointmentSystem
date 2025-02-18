using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IUserRepository
    {
        public  Task<ICollection<User>> GetUsers();
        public Task<User> GetUser(int userId);
        public Task<User> GetUser(string username);
        public Task<User> GetUserByName(string firstName, string lastName);
        public Task<User> GetUserByPerson(int personId);
        public Task<bool> UserExists(int userId);
        public Task<bool> SaveUser(User user);
        public Task<bool> UpdateUser(User user);
        public Task<bool> DeleteUser(User user);
    }
}
