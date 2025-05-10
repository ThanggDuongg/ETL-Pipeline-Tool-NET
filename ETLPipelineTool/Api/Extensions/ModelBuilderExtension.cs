namespace ETLPipelineTool.Api.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void ConfigTableName(this ModelBuilder modelBuilder)
        {
            var clrTypes = GetClrTypes(modelBuilder);
            foreach (var clrType in clrTypes)
            {
                modelBuilder.Entity(clrType).ToTable(clrType.Name);
            }
        }

        public static void ConfigAudit(this ModelBuilder modelBuilder)
        {
            var clrTypes = GetClrTypes(modelBuilder);
            var appliedOnClrTypes = clrTypes
                .Where(x => x.IsAssignableTo(typeof(IAuditing)))
                .Where(x =>
                    !clrTypes.Any(t => x.IsSubclassOf(t) && t.IsAssignableTo(typeof(IAuditing)))
                )
                .ToArray();
            foreach (var clrType in appliedOnClrTypes)
            {
                modelBuilder
                    .Entity(clrType)
                    .Property(nameof(IAuditing.CreatedBy))
                    .HasUnicodeTextColumn(50)
                    .IsRequired();
                modelBuilder
                    .Entity(clrType)
                    .Property(nameof(IAuditing.ModifiedBy))
                    .HasUnicodeTextColumn(50)
                    .IsRequired();
            }
        }

        public static void ConfigIdentity(this ModelBuilder modelBuilder)
        {
            var clrTypes = GetClrTypes(modelBuilder);
            var appliedOnClrTypes = clrTypes
                .Where(x => x.IsAssignableTo(typeof(IIdentity<Guid>)))
                .Where(x =>
                    !clrTypes.Any(t =>
                        x.IsSubclassOf(t) && t.IsAssignableTo(typeof(IIdentity<Guid>))
                    )
                )
                .ToArray();
            foreach (var clrType in appliedOnClrTypes)
            {
                modelBuilder.Entity(clrType).HasKey(nameof(IIdentity<Guid>.Id));
                modelBuilder
                    .Entity(clrType)
                    .Property(nameof(IIdentity<Guid>.Id))
                    .ValueGeneratedNever();
            }
        }

        public static void ConfigRowVersion(this ModelBuilder modelBuilder)
        {
            var clrTypes = GetClrTypes(modelBuilder);
            var appliedOnClrTypes = clrTypes
                .Where(x => x.IsAssignableTo(typeof(IVersioning)))
                .Where(x =>
                    !clrTypes.Any(t => x.IsSubclassOf(t) && t.IsAssignableTo(typeof(IVersioning)))
                )
                .ToArray();
            foreach (var clrType in appliedOnClrTypes)
            {
                modelBuilder
                    .Entity(clrType)
                    .Property(nameof(IVersioning.RowVersion))
                    .IsRowVersion();
            }
        }

        private static Type[] GetClrTypes(ModelBuilder builder)
        {
            return
            [
                .. builder.Model.GetEntityTypes().Select(e => e.ClrType).Where(t => !t.IsAbstract),
            ];
        }
    }
}
