using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IDepartmentRepository
    {
        public ICollection<Department> GetDepartments();
        public Department GetDepartment(int departmentId);
        public bool DepartmentExists(int departmentId);
        public bool SaveDepartment(Department departmentToSave);
        public bool UpdateDepartment(Department departmetnUpdated);
        public bool DeleteDepartment(Department departmentToDelete);
        public bool Save();
    }
}
