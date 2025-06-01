namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
  public class TransformRuleEntityConfig : IEntityTypeConfiguration<TransformRule>
  {
    public void Configure(EntityTypeBuilder<TransformRule> builder)
    {
      builder
        .Property(x => x.RuleType)
        .HasEnumToStringConversation()
        .HasUnicodeTextColumn(50)
        .IsRequired();
      builder.Property(x => x.RuleConfigurationJson).HasUnicodeTextColumn().IsRequired();
    }
  }
}
