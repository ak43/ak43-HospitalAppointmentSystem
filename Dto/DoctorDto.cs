using HospitalAppointmentSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Dto
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public string Availability { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
    }
}
