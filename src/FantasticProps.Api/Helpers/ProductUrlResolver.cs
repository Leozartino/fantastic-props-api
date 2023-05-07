using AutoMapper;
using Core.Entities;
using FantasticProps.Dtos;
using Microsoft.Extensions.Configuration;

namespace FantasticProps.Helpers;

public class ProductUrlResolver : IValueResolver<Product, ProductToDto, string>
{
    private readonly IConfiguration _configuration;

    public ProductUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string? Resolve(Product source, ProductToDto destination, 
        string destMember, ResolutionContext context)
    {
        return !string.IsNullOrEmpty(source.PictureUrl) ? 
            $"{_configuration["ApiUrl"]}/{source.PictureUrl}" 
            : null;
    }
}