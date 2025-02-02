using AutoMapper;
using HospitalAppointmentSystem.Dto;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Doctor, DoctorDto>();
            CreateMap<DoctorDto, Doctor>();
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<DoctorAvailability, DoctorAvailabilityDto>();
            CreateMap<DoctorAvailabilityDto, DoctorAvailability>();
        }
        
    }
}
