using HospitalAppointmentSystem.Models;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        //doctors(List of doctors in the department)

        public ICollection<Doctor> Doctors { get; set; }
    }
}
