namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
  public class EtlExecutionLogEntityConfig : IEntityTypeConfiguration<EtlExecutionLog>
  {
    public void Configure(EntityTypeBuilder<EtlExecutionLog> builder)
    {
      builder.Property(x => x.StartTime).IsRequired();
      builder.Property(x => x.EndTime);
      builder.Property(x => x.Status).HasEnumToStringConversation().HasAsciiColumn(50).IsRequired();
      builder.Property(x => x.ErrorMessage).HasUnicodeTextColumn(256);
      builder.Property(x => x.ExtractedRowCount).IsRequired();
      builder.Property(x => x.TransformedRowCount).IsRequired();
      builder.Property(x => x.LoadedRowCount).IsRequired();
      builder.Property(x => x.DurationMs).IsRequired();
    }
  }
}
