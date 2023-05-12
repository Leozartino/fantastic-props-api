using Core;
using Core.Entities;
using FantasticProps.Dtos;
using System.Collections.Generic;

namespace FantasticProps.Adapters
{
    public class ProductAdapter : IAdapter<IEnumerable<Product>, IEnumerable<ProductToDto>>
    {
        public IEnumerable<ProductToDto> Adapt(IEnumerable<Product> source)
        {
            throw new System.NotImplementedException();
        }
    }
}
