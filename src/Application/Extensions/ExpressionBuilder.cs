using System.Linq.Expressions;
using Application.Dto;
using SharedKernel;

namespace Application.Extensions
{
    public static class ExpressionBuilder
    {
        /// <summary>
        /// Creates a filter expression for the given entity type based on the provided search parameters.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="searchParams">The search parameters DTO.</param>
        /// <returns>An expression representing the filter.</returns>
        public static Expression<Func<TEntity, bool>> CreateFilter<TEntity>(SearchParamsDTO searchParams)
        {
            var parameter = Expression.Parameter(typeof(TEntity), typeof(TEntity).Name.ToLower());
            Expression body = Expression.Equal(
                Expression.Property(parameter, "IsActive"),
                Expression.Constant(true)
            );

            foreach (var filter in searchParams.SearchParams)
            {
                var propertyInfo = typeof(TEntity).GetProperty(filter.PropertyName);
                if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
                    continue;

                var property = Expression.Property(parameter, filter.PropertyName);
                var value = Expression.Constant(filter.Value, typeof(string));

                Expression filterExpr = filter.MatchType switch
                {
                    SearchType.Contains => Expression.Call(property, nameof(string.Contains), null, value),
                    SearchType.Exact => Expression.Equal(property, value),
                    SearchType.StartsWith => Expression.Call(property, nameof(string.StartsWith), null, value),
                    SearchType.EndsWith => Expression.Call(property, nameof(string.EndsWith), null, value),
                    _ => Expression.Constant(true)
                };
                body = Expression.AndAlso(body, filterExpr);
            }

            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }
    }
}
