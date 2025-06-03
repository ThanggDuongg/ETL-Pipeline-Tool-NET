namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
  public class AuditLogEntityConfig : IEntityTypeConfiguration<AuditLog>
  {
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
      builder.Property(x => x.TableName).HasUnicodeTextColumn(256).IsRequired();
      builder.Property(x => x.ActionType).HasAsciiColumn(20).IsRequired();
      builder.Property(x => x.KeyValues).HasUnicodeTextColumn().IsRequired();
      builder.Property(x => x.OldValues).HasUnicodeTextColumn();
      builder.Property(x => x.NewValues).HasUnicodeTextColumn();
    }
  }
}
