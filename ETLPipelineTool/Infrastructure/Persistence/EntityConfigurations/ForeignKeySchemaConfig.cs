namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
  public class ForeignKeySchemaConfig : IEntityTypeConfiguration<ForeignKeySchema>
  {
    public void Configure(EntityTypeBuilder<ForeignKeySchema> builder)
    {
      builder.Property(x => x.ConstraintName).HasAsciiColumn(200).IsRequired();
      builder.Property(x => x.PrincipalTable).HasAsciiColumn(200).IsRequired();
    }
  }
}
