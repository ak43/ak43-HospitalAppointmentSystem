namespace HospitalAppointmentSystem.Models
{
    public class UserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int PersonId { get; set; }

        // Navigation property
        public Person Person { get; set; }
    }
}
