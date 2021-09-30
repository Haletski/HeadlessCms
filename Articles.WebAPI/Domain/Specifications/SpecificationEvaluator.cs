using Articles.WebAPI.Domain.Entities;
using Articles.WebAPI.Domain.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Articles.WebAPI.Domain.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> SpecificationQuery<TEntity>(this IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
            where TEntity : BaseEntity
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.IsOrderingEnabled)
            {
                query = query.ApplyOrdering(specification);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                    .Take(specification.Take);
            }

            return query;
        }

        private static IQueryable<TEntity> ApplyOrdering<TEntity>(this IQueryable<TEntity> query, ISpecification<TEntity> specification) 
            where TEntity : BaseEntity
        {
            return specification.OrderType.ToLower() switch
            {
                "asc" => query.OrderBy(specification.OrderBy),

                "desc" => query.OrderByDesc(specification.OrderBy),

                _ => query,
            };
        }

        private static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string fieldName)
        {
            return source.ApplyOrderBy(fieldName, "OrderBy");
        }

        private static IQueryable<T> OrderByDesc<T>(this IQueryable<T> source, string fieldName)
        {
            return source.ApplyOrderBy(fieldName, "OrderByDescending");
        }

        private static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> source, string fieldName, string orderType)
        {
            var type = typeof(T);
            var property = type.GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            var resultExp = Expression.Call(typeof(Queryable), orderType, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));

            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
