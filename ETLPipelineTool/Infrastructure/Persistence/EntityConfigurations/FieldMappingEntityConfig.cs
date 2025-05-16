namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
    public class FieldMappingEntityConfig : IEntityTypeConfiguration<FieldMapping>
    {
        public void Configure(EntityTypeBuilder<FieldMapping> builder)
        {
            builder.Property(x => x.TargetField).HasUnicodeTextColumn(256).IsRequired();

            builder.Property(x => x.TransformRuleType).HasUnicodeTextColumn(100).IsRequired();

            builder.Property(x => x.TransformConfig).HasUnicodeTextColumn();

            builder.Property(x => x.Order).IsRequired();

            builder
                .Property(x => x.SourceFields)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .HasUnicodeTextColumn()
                .IsRequired()
                .Metadata.SetValueComparer(
                    new ValueComparer<ICollection<string>>(
                        (c1, c2) =>
                            ReferenceEquals(c1, c2)
                            || (
                                c1 != null
                                && c2 != null
                                && c1.Count == c2.Count
                                && c1.OrderBy(x => x).SequenceEqual(c2)
                            ),
                        c =>
                            c == null
                                ? 0
                                : c.OrderBy(x => x)
                                    .Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                );
        }
    }
}
