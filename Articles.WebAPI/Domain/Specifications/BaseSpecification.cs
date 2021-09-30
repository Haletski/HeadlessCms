using Articles.WebAPI.Domain.Entities;
using Articles.WebAPI.Domain.Interfaces;
using System;
using System.Linq.Expressions;

namespace Articles.WebAPI.Domain.Specifications
{
    public class BaseSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : BaseEntity
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected BaseSpecification()
        {
        }

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public string OrderBy { get; private set; }

        public string OrderType { get; private set; }

        public bool IsOrderingEnabled { get; private set; }

        protected virtual void ApplyPaging(int page, int pageSize)
        {
            Skip = (page - 1) * pageSize;
            Take = pageSize;

            IsPagingEnabled = true;
        }

        protected virtual void ApplyOrderBy(string fieldName, string orderType)
        {
            OrderBy = fieldName;
            OrderType = orderType;

            IsOrderingEnabled = true;
        }
    }
}
