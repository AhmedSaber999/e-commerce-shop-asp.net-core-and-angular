using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criateria)
        {
            Criateria = criateria;
        }
        public Expression<Func<T, bool>> Criateria {get; }

        public List<Expression<Func<T, object>>> Includes{get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get; private set;}

        public Expression<Func<T, object>> OrderByDescending  {get; private set;}

        public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool IsPagingEnabled {get; private set;}

        protected void AddInclude(Expression<Func<T, object>> include_expression)
        {
            Includes.Add(include_expression);
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderby_expression)
        {
            OrderBy = orderby_expression;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> orderby_descending_expression)
        {
            OrderByDescending = orderby_descending_expression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}