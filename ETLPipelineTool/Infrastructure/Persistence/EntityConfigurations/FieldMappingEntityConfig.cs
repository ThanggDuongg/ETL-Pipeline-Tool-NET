namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
    public class FieldMappingEntityConfig : IEntityTypeConfiguration<FieldMapping>
    {
        public void Configure(EntityTypeBuilder<FieldMapping> builder)
        {
            builder.Property(x => x.TargetField).HasUnicodeTextColumn(256).IsRequired();

            builder.Property(x => x.Order).IsRequired();
        }
    }
}
