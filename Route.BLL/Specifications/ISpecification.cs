﻿using System.Linq.Expressions;

namespace Route.BLL.Specifications
{
    public interface ISpecification<T>
    {

        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }

        public Expression<Func<T, object>> OrderBy { get; }
        public Expression<Func<T, object>> OrderByDescending { get; }

        public int Take { get; }
        public int Skip { get; }

        public bool IsPaging { get; }


    }
}
