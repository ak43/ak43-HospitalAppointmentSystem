using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context)
        {
        _context = context;
        }
        public ICollection<Department> GetDepartments()
        {
            return _context.Departments.OrderBy(d => d.Name).ToList();
        }
        public Department GetDepartment(int departmentId)
        {
            return _context.Departments.Where(d => d.Id == departmentId).FirstOrDefault();        }

        public bool DepartmentExists(int departmentId)
        {
            return _context.Departments.Any(d => d.Id == departmentId);
        }

        public bool SaveDepartment(Department department)
        {
            _context.Add(department);
        return Save();}

        public bool UpdateDepartment(Department department)
        {
            _context.Update(department);
            return Save();
        }
        public bool DeleteDepartment(Department department)
        {
            _context.Remove(department);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
