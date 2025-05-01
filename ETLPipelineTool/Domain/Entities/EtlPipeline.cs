namespace ETLPipelineTool.Domain.Entities
{
    public class EtlPipeline : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public PipelineSourceType SourceType { get; set; }
        public PipelineTargetType TargetType { get; set; }
        public string SourceConfigurationJson { get; set; } = default!;
        public string TargetConfigurationJson { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public ICollection<PipelineSchedule> PipelineSchedules { get; set; } = [];
        public ICollection<FieldMapping> FieldMappings { get; set; } = [];
        public ICollection<TransformRule> TransformRules { get; set; } = [];
        public ICollection<EtlExecutionLog> EtlExecutionLogs { get; set; } = [];
    }
}
