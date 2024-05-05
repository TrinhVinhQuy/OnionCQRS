using AutoMapper;
using OnionCQRS.Application.DTOs.Product;
using OnionCQRS.Domain.Entities;

namespace OnionCQRS.Application.Configuration
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
