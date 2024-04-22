using AutoMapper;
using EmployeesAPI.Models.Dto;
using EmployeesAPI.Models.Database;

namespace EmployeesAPI.Profiles
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Passport, PassportDto>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.DepartmentPhone));

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Passport, opt => opt.MapFrom(
                    src => new PassportDto { Number = src.Passport.Number, Type = src.Passport.Type }))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(
                    src => new DepartmentDto { Name = src.Department.DepartmentName, Phone = src.Department.DepartmentPhone }));
            
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.Passport, opt => opt.MapFrom(
                    src => new Passport { Type = src.Passport.Type, Number = src.Passport.Number }))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(
                    src => new Department { DepartmentPhone = src.Department.Phone, DepartmentName = src.Department.Name }));

            CreateMap<EmployeeShortInfo, EmployeeDto>()
                .ForMember(dest => dest.Passport, opt => opt.MapFrom(
                    src => new PassportDto { Type = src.Type, Number = src.Number }))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(
                    src => new DepartmentDto { Name = src.DepartmentName, Phone = src.DepartmentPhone }));
        }
    }
}
