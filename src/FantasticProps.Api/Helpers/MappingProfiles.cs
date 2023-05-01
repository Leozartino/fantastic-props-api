using AutoMapper;
using Core.Entities;
using FantasticProps.Dtos;

namespace FantasticProps.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToDto>();
    }
}