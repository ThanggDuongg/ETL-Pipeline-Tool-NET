namespace ETLPipelineTool.Shared.Helpers
{
    public static class ProjectionHelper
    {
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
