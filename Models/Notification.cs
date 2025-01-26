using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Models
{
    [PrimaryKey("Id")]
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime NotificationDate { get; set; }

 
    }
}
