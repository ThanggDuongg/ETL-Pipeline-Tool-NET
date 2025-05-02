using ETLPipelineTool.Domain.Entities.Abstracts;

namespace ETLPipelineTool.Domain.Entities
{
    public class PipelineSchedule : BaseEntity
    {
        public Guid EtlPipelineId { get; set; }
        public EtlPipeline? EtlPipeline { get; set; }
        public string CronExpression { get; set; } = default!;
        public bool IsEnabled { get; set; } = true;
    }
}
