using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementMVC.DAL.Specification
{
    namespace CompanyManagementMVC.DAL.Specification
    {
        public class BaseSpecification<T> : ISpecification<T>
        {
            public Expression<Func<T, bool>> Criteria { get; protected set; } = null!;
            public List<Expression<Func<T, object>>> Includes { get; } = new();
            public List<string> IncludeStrings { get; } = new();
            public Expression<Func<T, object>> OrderBy { get; protected set; } = null!;
            public Expression<Func<T, object>> OrderByDescending { get; protected set; } = null!;
            public int Take { get; protected set; }
            public int Skip { get; protected set; }
            public bool IsPagingEnabled { get; protected set; }

            // Where
            protected void AddCriteria(Expression<Func<T, bool>> criteria) => Criteria = criteria;

            // Include
            protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);
            protected void AddInclude(string include) => IncludeStrings.Add(include);

            // Order
            protected void AddOrderBy(Expression<Func<T, object>> orderBy) => OrderBy = orderBy;
            protected void AddOrderByDescending(Expression<Func<T, object>> orderByDesc) => OrderByDescending = orderByDesc;

            // Pagination
            protected void ApplyPaging(int skip, int take)
            {
                Skip = skip;
                Take = take;
                IsPagingEnabled = true;
            }
        }
    }
}
