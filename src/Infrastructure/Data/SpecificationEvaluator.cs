using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, 
        ISpecification<TEntity> specification)
    {
        var query = inputQuery;
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }
        
        query = specification.Includes.Aggregate
            (query, (current, include) 
                => current.Include(include));
        
        return query;
    }
}