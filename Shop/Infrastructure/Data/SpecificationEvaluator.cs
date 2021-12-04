using System.Linq;
using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> input_query, ISpecification<TEntity> specification)
        {
            var query = input_query;
            if(specification.Criateria != null)
            {
                query = query.Where(specification.Criateria);
            }
            
            if(specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if(specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if(specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }

    }
}