namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
    public class ForeignKeyPrincipalColumnConfig
        : IEntityTypeConfiguration<ForeignKeyPrincipalColumn>
    {
        public void Configure(EntityTypeBuilder<ForeignKeyPrincipalColumn> builder)
        {
            builder.HasKey(x => new { x.ForeignKeySchemaId, x.PrincipalColumnName });
            builder.Property(x => x.PrincipalColumnName).HasAsciiColumn(200).IsRequired();
        }
    }
}
