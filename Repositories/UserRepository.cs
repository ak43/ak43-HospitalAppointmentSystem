using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return await _context.Users.OrderBy(u => u.Username).ToListAsync();
        }
        public async Task<User> GetUser(int userId)
        {
            return await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<User> GetUser(string username)
        {
            return await _context.Users
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByName(string firstName, string lastName)
        {
            return await _context.Users
                .Where(u => u.Person.FirstName.Contains(firstName) && u.Person.LastName.Contains(lastName))
                .FirstOrDefaultAsync();
        }
        public async Task<bool> UserExists(int userId)
        {
            //var users = await _context.Users.Any(u => u.Id ==  userId);

            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> SaveUser(User user)
        {
            await _context.Users.AddAsync(user);
        return await Save();
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Users.Update(user);
            return await Save();
        }

        public async Task<bool> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<User> GetUserByPerson(int personId)
        {
            return await _context.Users.Where(u => u.PersonId == personId).FirstOrDefaultAsync();
                }
    }
}
