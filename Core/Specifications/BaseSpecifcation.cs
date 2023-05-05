﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecifcation<T> : ISpecification<T>
    {
        public BaseSpecifcation(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
                => Includes.Add(includeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
                => OrderBy=OrderByExpression;
        protected void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
                => OrderBy = OrderByDescExpression;

    }
}