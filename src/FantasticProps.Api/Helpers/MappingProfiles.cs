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
                memberConfigSource
                    => memberConfigSource.MapFrom(product => product.ProductBrand!.Name))
            .ForMember(productToDto => productToDto.ProductType,
                memberConfigSource
                    => memberConfigSource.MapFrom(product => product.ProductType!.Name))
            .ForMember(productToDto => productToDto.PictureUrl, 
                memberConfigSource
                => memberConfigSource.MapFrom<ProductUrlResolver>());
    }
} 