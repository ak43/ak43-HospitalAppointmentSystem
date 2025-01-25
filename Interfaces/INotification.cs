namespace HospitalAppointmentSystem.Interfaces
{
    public interface INotification
    {
        public void sendConfirmation();
        public void sendReminder();
    }
}
