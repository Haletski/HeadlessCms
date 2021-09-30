using Articles.WebAPI.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace Articles.WebAPI.Domain.Interfaces
{
    public interface ISpecification<TEntity>
        where TEntity : BaseEntity
    {
        Expression<Func<TEntity, bool>> Criteria { get; }

        string OrderBy { get; }

        string OrderType { get; }

        bool IsOrderingEnabled { get; }

        int Take { get; }

        int Skip { get; }

        bool IsPagingEnabled { get; }
    }
}
