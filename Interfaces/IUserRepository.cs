using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IUserRepository
    {
        public ICollection<User> GetUsers();
        public User GetUser(int userId);
        public User GetUser(string username);
        public User GetUserByName(string firstName, string lastName);
        public User GetUserByPerson(int personId);
        public bool UserExists(int userId);
        public bool SaveUser(User user);
        public bool UpdateUser(User user);
        public bool DeleteUser(User user);
    }
}
