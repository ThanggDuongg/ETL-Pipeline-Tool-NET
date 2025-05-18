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

            var body = BuildInitExpression(typeof(TEntity), typeof(TDto), parameter, fields);
            return Expression.Lambda<Func<TEntity, TDto>>(body, parameter);
        }

        private static Expression BuildInitExpression(
            Type sourceType,
            Type targetType,
            Expression sourceExpression,
            ICollection<string> fields
        )
        {
            var constructor =
                targetType.GetConstructor(Type.EmptyTypes)
                ?? throw new InvalidOperationException(
                    $"{targetType.Name} must have a parameterless constructor."
                );

            var groupedFields = fields
                .Where(f => !string.IsNullOrWhiteSpace(f))
                .Select(f => f.Trim())
                .GroupBy(f => f.Split('.')[0])
                .ToDictionary(g => g.Key, g => g.ToList());

            var bindings = new List<MemberBinding>();

            foreach (var kvp in groupedFields)
            {
                var propName = kvp.Key;
                var subFields = kvp
                    .Value.Select(f =>
                    {
                        var parts = f.Split('.', 2);
                        return parts.Length > 1 ? parts[1] : null;
                    })
                    .Where(f => f != null)
                    .Select(f => f!)
                    .ToList();

                var sourceProp = sourceType.GetProperty(propName);
                var targetProp = targetType.GetProperty(propName);

                if (sourceProp == null || targetProp == null)
                {
                    continue;
                }

                Expression sourcePropAccess = Expression.Property(sourceExpression, sourceProp);

                if (subFields.Count != 0)
                {
                    var nestedInit = BuildInitExpression(
                        sourceProp.PropertyType,
                        targetProp.PropertyType,
                        sourcePropAccess,
                        subFields
                    );
                    bindings.Add(Expression.Bind(targetProp, nestedInit));
                }
                else
                {
                    bindings.Add(Expression.Bind(targetProp, sourcePropAccess));
                }
            }

            return Expression.MemberInit(Expression.New(constructor), bindings);
        }
    }
}
