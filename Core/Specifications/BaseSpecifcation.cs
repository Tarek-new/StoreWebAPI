using Core.Enitities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecifcation<T> : ISpecification<T> where T : BaseEntity
    {

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPagingEnabled { get; private set; }


        protected void AddInclude(Expression<Func<T, object>> includeExpression)
                        => Includes.Add(includeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
                => OrderBy = OrderByExpression;
        protected void AddOrderByDesc(Expression<Func<T, object>> OrderByDescExpression)
                => OrderBy = OrderByDescExpression;

        protected void Paging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        public BaseSpecifcation(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
    }
}
