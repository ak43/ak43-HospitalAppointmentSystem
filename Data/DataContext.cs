using HospitalAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentSystem.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {   
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }  
        public DbSet<Department> Departments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorPatient> DoctorPatients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Doctor ↔ Appointment relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Configure the Patient ↔ Appointment relationship
            modelBuilder.Entity<Appointment>()
                    .HasOne(a => a.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Configure the Doctor ↔ DoctorAvailability relationship
            modelBuilder.Entity<DoctorAvailability>()
                    .HasOne(da => da.Doctor)
                    .WithMany(d => d.Availabilities)
                    .HasForeignKey(da => da.DoctorId)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete for availability slots
        }
    }
}
