using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAppointmentSystem.Models
{
    [PrimaryKey("Id")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Sex { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set;}

        public string username { get; set; }
        public string password { get; set; }

        
    }
}

