namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
    public class EtlPipelineEntityConfig : IEntityTypeConfiguration<EtlPipeline>
    {
        public void Configure(EntityTypeBuilder<EtlPipeline> builder)
        {
            builder.Property(x => x.Name).HasUnicodeTextColumn(50).IsRequired();
            builder.Property(x => x.Description).HasUnicodeTextColumn(256).IsRequired();
            builder
                .Property(x => x.SourceType)
                .HasEnumToStringConversation()
                .HasAsciiColumn(50)
                .IsRequired();
            builder
                .Property(x => x.TargetType)
                .HasEnumToStringConversation()
                .HasAsciiColumn(50)
                .IsRequired();
            builder.Property(x => x.SourceConfigurationJson).HasUnicodeTextColumn().IsRequired();
            builder.Property(x => x.TargetConfigurationJson).HasUnicodeTextColumn().IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true).IsRequired();
        }
    }
}
