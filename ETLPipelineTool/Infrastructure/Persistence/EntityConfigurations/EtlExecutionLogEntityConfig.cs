namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
    public class EtlExecutionLogEntityConfig : IEntityTypeConfiguration<EtlExecutionLog>
    {
        public void Configure(EntityTypeBuilder<EtlExecutionLog> builder)
        {
            builder
                .Property(x => x.Status)
                .HasEnumToStringConversation()
                .HasAsciiColumn(50)
                .IsRequired();
            builder.Property(x => x.ErrorMessage).HasUnicodeTextColumn(256);
        }
    }
}
