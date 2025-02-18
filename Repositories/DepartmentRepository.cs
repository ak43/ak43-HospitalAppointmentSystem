using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context)
        {
        _context = context;
        }
        public async Task<ICollection<Department>> GetDepartments()
        {
            //return await _context.Departments.ToListAsync();
            return await _context.Departments.OrderBy(d => d.Name).ToListAsync();

        }
        public async Task<Department> GetDepartment(int departmentId)
        {
            return await _context.Departments
                .Where(d => d.Id == departmentId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DepartmentExists(int departmentId)
        {
            return await _context.Departments.AnyAsync(d => d.Id == departmentId);
        }

        public async Task<bool> SaveDepartment(Department department)
        {
            await _context.AddAsync(department);
            return await Save();
        }

        public async Task<bool> UpdateDepartment(Department department)
        {
            _context.Update(department);
            return await Save();
        }
        public async Task<bool> DeleteDepartment(Department department)
        {
            _context.Remove(department);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
