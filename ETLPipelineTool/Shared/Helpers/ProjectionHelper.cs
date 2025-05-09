namespace ETLPipelineTool.Shared.Helpers
{
    public static class ProjectionHelper
    {
        /// <summary>
        /// Builds a dynamic selector that maps only the specified <paramref name="fields"/>
        /// from <typeparamref name="TEntity"/> to <typeparamref name="TDto"/>.
        /// </summary>
        /// <typeparam name="TEntity">The source entity type (must inherit from BaseEntity).</typeparam>
        /// <typeparam name="TDto">
        /// The destination DTO type. Must have a parameterless constructor and settable properties.
        /// </typeparam>
        /// <param name="fields">List of property names to include in the projection.</param>
        /// <returns>Expression that maps selected fields to a new <typeparamref name="TDto"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <typeparamref name="TDto"/> doesn't have a parameterless constructor.
        /// </exception>
        /// <remarks>
        /// Make sure <typeparamref name="TDto"/> is a record *object-style* (not positional).
        /// All mapped properties must be settable (e.g. with `init`) and there must be an empty constructor.
        /// </remarks>
        public static Expression<Func<TEntity, TDto>> CreateDynamicSelector<TEntity, TDto>(
            ICollection<string> fields
        )
            where TDto : class
            where TEntity : BaseEntity
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var bindings = new List<MemberBinding>();

            foreach (var propName in fields)
            {
                var entityProp = typeof(TEntity).GetProperty(propName);
                var dtoProp = typeof(TDto).GetProperty(propName);
                if (entityProp == null || dtoProp == null)
                {
                    continue;
                }

                var entityPropAccess = Expression.Property(parameter, entityProp);
                var bind = Expression.Bind(dtoProp, entityPropAccess);
                bindings.Add(bind);
            }

            var constructor =
                typeof(TDto).GetConstructor(Type.EmptyTypes)
                ?? throw new InvalidOperationException(
                    $"{typeof(TDto).Name} must have a parameterless constructor"
                );

            // Workaround for "record class"(object-style record)
            // Record must have at least empty constructor
            var body = Expression.MemberInit(Expression.New(constructor), bindings);
            return Expression.Lambda<Func<TEntity, TDto>>(body, parameter);
        }
    }
}
