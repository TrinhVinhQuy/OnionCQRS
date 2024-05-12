using AutoMapper;
using OnionCQRS.Application.DTOs.Employee;
using OnionCQRS.Application.DTOs.Product;
using OnionCQRS.Domain.Entities;

namespace OnionCQRS.Application.Configuration
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<EmployeeDTO, EmployeeDetailDTO>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap();
        }
    }
}
