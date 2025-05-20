namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
    public class TableSchemaConfig : IEntityTypeConfiguration<TableSchema>
    {
        public void Configure(EntityTypeBuilder<TableSchema> builder)
        {
            builder.Property(x => x.TableName).HasAsciiColumn(200).IsRequired();
        }
    }
}
