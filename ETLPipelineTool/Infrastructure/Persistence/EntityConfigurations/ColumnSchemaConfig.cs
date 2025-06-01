namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
  public class ColumnSchemaConfig : IEntityTypeConfiguration<ColumnSchema>
  {
    public void Configure(EntityTypeBuilder<ColumnSchema> builder)
    {
      builder.Property(x => x.ColumnName).HasAsciiColumn(200).IsRequired();
      builder.Property(x => x.DataType).HasAsciiColumn(100).IsRequired();
      builder.Property(x => x.IsPrimaryKey).HasDefaultValue(false).IsRequired();
      builder.Property(x => x.IsNullable).HasDefaultValue(false).IsRequired();
    }
  }
}
