using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    // 3mlt abstract l2n msh ha5od mno object 
 
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Includes
        public ICollection<Expression<Func<TEntity, object>>> IncludesExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExp)
        {
            IncludesExpressions.Add(IncludeExp);
        }
        #endregion

        #region Filteration
        public Expression<Func<TEntity, bool>> Criteria { get; }
        protected BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        #endregion


        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
        #endregion


        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        // total count = 40
        // page size = 10 
        // 10 , 10 , 10 , 10 
        //page index = 3 , =  (page index - 1) * page size 
        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }
        public bool IsPaginated { get; private set; }
        #endregion


    }
}
