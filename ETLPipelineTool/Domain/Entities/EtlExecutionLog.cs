using ETLPipelineTool.Domain.Entities.Abstracts;

namespace ETLPipelineTool.Domain.Entities
{
    public class EtlExecutionLog : BaseEntity
    {
        public Guid EtlPipelineId { get; set; }
        public EtlPipeline? EtlPipeline { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public EtlExecutionStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
