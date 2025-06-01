namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
  public class EtlExecutionLogEntityConfig : IEntityTypeConfiguration<EtlExecutionLog>
  {
    public void Configure(EntityTypeBuilder<EtlExecutionLog> builder)
    {
      builder.Property(x => x.Status).HasEnumToStringConversation().HasAsciiColumn(50).IsRequired();
      builder.Property(x => x.ErrorMessage).HasUnicodeTextColumn(256);
      builder.Property(x => x.RecordsProcessed).IsRequired();
      builder.Property(x => x.RecordsSucceeded).IsRequired();
      builder.Property(x => x.RecordsFailed).IsRequired();
      builder.Property(x => x.ErrorDetails).HasUnicodeTextColumn();
      builder.Property(x => x.ProcessingTimeMs).IsRequired();
    }
  }
}
