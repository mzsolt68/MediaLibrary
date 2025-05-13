namespace SharedKernel.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the value of a property by name from the specified entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity instance.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value of the property, or null if not found.</returns>
        public static object? GetPropertyValue<T>(this T entity, string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            return property?.GetValue(entity);
        }
    }
}
