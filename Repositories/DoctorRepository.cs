using HospitalAppointmentSystem.Data;
using HospitalAppointmentSystem.Interfaces;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _context;

        public DoctorRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Doctor> GetDoctors()
        {
            return _context.Doctors.OrderBy(d => d.FirstName).ToList();
            //return _context.Doctors.ToList();
        }

        public Doctor GetDoctor(int doctorID)
        {
            return _context.Doctors.Where(d => d.Id == doctorID).FirstOrDefault();
        }

        public ICollection<Doctor> GetDoctors(string firstName)
        {
            return _context.Doctors.Where(d => d.FirstName.Contains(firstName)).ToList();
        }

        public ICollection<Doctor> GetDoctorByDepartment(int departmentId)
        {
            return _context.Doctors.Where(d => d.DepartmentId == departmentId).ToList();
        }

        public ICollection<Doctor> GetDoctorBySpeciality(string speciality)
        {
            return _context.Doctors.Where(d => d.Specialization == speciality).ToList();
        }

        public bool CreateDoctor(Doctor doctor)
        {
            //var pokemonOwnerEntity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            //var category = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            //var pokemonOwner = new PokemonOwner
            //{
            //    Owner = pokemonOwnerEntity,
            //    Pokemon = pokemon
            //};

            //_context.Add(pokemonOwner);

            //var pokemonCategory = new PokemonCategory
            //{
            //    Category = category,
            //    Pokemon = pokemon
            //};

            //_context.Add(pokemonCategory);

            //_context.Add(pokemon);
            //return Save();

            _context.Add(doctor);
            return Save();
        }


        public bool UpdateDoctor(Doctor doctor)
        {
            _context.Update(doctor);
            return Save();
        }

        public bool DeleteDoctor(Doctor doctor)
        {
            _context.Remove(doctor);
            return Save();
        }

        public bool DoctorExists(int doctorId)
        {
            return _context.Doctors.Any(d => d.Id == doctorId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public ICollection<Appointment> GetAppointments()
        {
            throw new NotImplementedException();
        }

        public ICollection<Appointment> GetAppointments(int doctorId)
        {
            throw new NotImplementedException();
        }

        public bool SetAvailability(int doctorId, string availability)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAvailability(int doctorId, string availability)
        {
            throw new NotImplementedException();
        }
        public bool DeleteAvailability(int doctorId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Availability> GetDoctorAvailability(int doctorId)
        {
            throw new NotImplementedException();
        }
    }
}
