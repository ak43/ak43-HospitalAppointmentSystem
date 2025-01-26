using HospitalAppointmentSystem.Models;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models
{
    [PrimaryKey("Id")]
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } 
        //doctors(List of doctors in the department)

        public ICollection<Doctor> Doctors { get; set; }
    }
}
