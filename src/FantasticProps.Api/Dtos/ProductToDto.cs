using System;

namespace FantasticProps.Dtos;

public class ProductToDto
{
    public Guid Id { get; set; }
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public string? PictureUrl { get; private set; }
    public string? ProductType { get; private set; }
    public string? ProductBrand { get; private set; }
} 