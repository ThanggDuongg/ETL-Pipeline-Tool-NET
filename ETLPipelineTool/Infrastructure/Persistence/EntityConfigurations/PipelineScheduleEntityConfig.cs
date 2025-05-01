namespace ETLPipelineTool.Infrastructure.Persistence.EntityConfigurations
{
    public class PipelineScheduleEntityConfig : IEntityTypeConfiguration<PipelineSchedule>
    {
        public void Configure(EntityTypeBuilder<PipelineSchedule> builder)
        {
            builder.Property(x => x.CronExpression).HasUnicodeTextColumn(20).IsRequired();
            builder.Property(x => x.IsEnabled).HasDefaultValue(true).IsRequired();
        }
    }
}
