using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    internal static class SpecificationsEvaluater
    {
        //bulid Query 

        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint ,
            ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = EntryPoint;
            // _dbContext.Products

            if(specifications is not null)
            {

                if(specifications.Criteria is not null)
                {
                    Query = Query.Where(specifications.Criteria);
                }

                if(specifications.IncludesExpressions is not null && specifications.IncludesExpressions.Any())
                {
                    foreach(var IncludesExp in specifications.IncludesExpressions)
                    {
                        Query = specifications.IncludesExpressions.Aggregate(Query,
                                (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
                    }
                }

                if(specifications.OrderBy is not null)
                {
                    Query = Query.OrderBy(specifications.OrderBy);
                }

                if (specifications.OrderByDescending is not null)
                {
                    Query = Query.OrderByDescending(specifications.OrderByDescending);
                }

                if(specifications.IsPaginated)
                {
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);
                }

            }



            return Query;
        }
    }
}
