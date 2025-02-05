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
        public DbSet<Availability> Availability { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasKey(d => d.Id);
      
            modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Department)
            .WithMany(dept => dept.Doctors)
            .HasForeignKey(da => da.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction); // Disable cascade delete

            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Availabilities)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Patient>()
       .HasOne(p => p.Doctor)
       .WithMany(d => d.Patients)
       .HasForeignKey(p => p.DoctorId)
       .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction instead of Cascade


            // Configure the Doctor ↔ Appointment relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(p => p.Appointments)
                //.WithMany(d => d.Patients)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction); 

            // Configure the Patient ↔ Appointment relationship
            modelBuilder.Entity<Appointment>()
                    .HasOne(a => a.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            //modelBuilder.Entity<Doctor>()
            //.HasOne(d => d.Availabilities)
            //.WithOne(da => da.Doctor)
            //.HasForeignKey<Availability>(da => da.DoctorId)
            //.OnDelete(DeleteBehavior.Restrict); // Disable cascade delete

            

            

            modelBuilder.Entity<DoctorPatient>()
    .HasOne(dp => dp.Doctor)
    .WithMany()
    .HasForeignKey(dp => dp.DoctorId)
    .OnDelete(DeleteBehavior.NoAction);  // Change cascade to no action

            modelBuilder.Entity<DoctorPatient>()
                .HasOne(dp => dp.Patient)
                .WithMany()
                .HasForeignKey(dp => dp.PatientId)
                .OnDelete(DeleteBehavior.Cascade);  // Keep cascade on Patient

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Doctor)
                .WithMany()
                .HasForeignKey(f => f.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure the Doctor ↔ DoctorAvailability relationship
            //modelBuilder.Entity<DoctorAvailability>()
            //        .HasOne(da => da.Doctor)
            //        .WithOne(d => d.Availabilities)
            //        .HasForeignKey(da => da.DoctorId)
            //        .OnDelete(DeleteBehavior.NoAction); // Cascade delete for availability slots
        }
    }
}
