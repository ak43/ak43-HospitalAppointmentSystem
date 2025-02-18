using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Interfaces
{
    public interface IDepartmentRepository
    {
        public  Task<ICollection<Department>> GetDepartments();
        public  Task<Department> GetDepartment(int departmentId);
        public  Task<bool> DepartmentExists(int departmentId);
        public  Task<bool> SaveDepartment(Department departmentToSave);
        public  Task<bool> UpdateDepartment(Department departmetnUpdated);
        public  Task<bool> DeleteDepartment(Department departmentToDelete);
        public Task<bool> Save();
    }
}
