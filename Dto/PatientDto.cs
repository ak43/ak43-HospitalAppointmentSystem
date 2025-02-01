namespace HospitalAppointmentSystem.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MRN { get; set; }
        public int Age { get; set; }
        public string MedicalHistory { get; set; }
        public int DoctorId { get; set; }
    }
}
