namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
    public class ForeignKeyColumnConfig : IEntityTypeConfiguration<ForeignKeyColumn>
    {
        public void Configure(EntityTypeBuilder<ForeignKeyColumn> builder)
        {
            builder.HasKey(x => new { x.ForeignKeySchemaId, x.ColumnName });
            builder.Property(x => x.ColumnName).HasAsciiColumn(200).IsRequired();
        }
    }
}
