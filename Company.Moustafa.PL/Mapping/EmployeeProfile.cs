using AutoMapper;
using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;

namespace Company.Moustafa.PL.Mapping
{
    public class EmployeeProfile :Profile
    {

        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>().ForMember(
                d=>d.Name,
                o=>o.MapFrom(s=>s.EmpName));
            CreateMap<Employee, CreateEmployeeDto>();

        }
    }

}
