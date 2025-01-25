using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml;

namespace HospitalAppointmentSystem.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime NotificationDate { get; set; }

 
    }
}
