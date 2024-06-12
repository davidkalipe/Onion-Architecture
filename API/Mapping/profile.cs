using API.DTO;
using AutoMapper;
using Domain.Entities;

namespace API.Mapping;

public class profile : Profile
{
    public profile()
    {
        CreateMap<Product, GetAllProductDto>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap();
    }
    
}