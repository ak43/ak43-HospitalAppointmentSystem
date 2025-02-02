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
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Patient>()
       .HasOne(p => p.Doctor)
       .WithMany(d => d.Patients)
       .HasForeignKey(p => p.DoctorId)
       .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction instead of Cascade
            

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

            modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Availabilities)
            .WithOne(da => da.Doctor)
            .HasForeignKey<DoctorAvailability>(da => da.DoctorId)
            .OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

            // Configure the Doctor ↔ DoctorAvailability relationship
            //modelBuilder.Entity<DoctorAvailability>()
            //        .HasOne(da => da.Doctor)
            //        .WithOne(d => d.Availabilities)
            //        .HasForeignKey(da => da.DoctorId)
            //        .OnDelete(DeleteBehavior.NoAction); // Cascade delete for availability slots
        }
    }
}
