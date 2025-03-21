using AutoMapper;
using Company.Moustafa.DAL.Models;
using Company.Moustafa.PL.Dtos;

namespace Company.Moustafa.PL.Mapping
{
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, CreateDepartmentDto>().ReverseMap();
        }
    }
}
