using System.Linq.Expressions;

namespace SharedKernel.Extensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Combines two expressions with a logical AND (AndAlso).
        /// </summary>
        /// <typeparam name="T">The type of the parameter in the expressions.</typeparam>
        /// <param name="expr1">The first expression.</param>
        /// <param name="expr2">The second expression.</param>
        /// <returns>
        /// A new expression representing the logical AND of the two input expressions.
        /// </returns>
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
