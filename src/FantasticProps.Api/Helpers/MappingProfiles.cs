using AutoMapper;
using Core.Entities;
using FantasticProps.Dtos;

namespace FantasticProps.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToDto>()
            .ForMember(productToDto => productToDto.ProductBrand, 
                product 
                     => product.MapFrom(p => p.ProductBrand.Name))
            .ForMember(productToDto => productToDto.ProductType, 
                product 
                    => product.MapFrom(p => p.ProductType.Name));
    }
    
} 