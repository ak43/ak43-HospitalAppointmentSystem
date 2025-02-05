namespace HospitalAppointmentSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int PersonId { get; set; }

        // Navigation property
        public Person Person { get; set; }
    }
}
