namespace ETLPipelineTool.Domain.Entities
{
    public class FieldMapping : BaseEntity
    {
        public Guid EtlPipelineId { get; set; }
        public EtlPipeline? EtlPipeline { get; set; }
        public int Order { get; set; } // Priority
        public ICollection<string> SourceFields { get; set; } = [];
        public string TargetField { get; set; } = default!;
        public string TransformRuleType { get; set; } = "Identity"; // Concat, IfNull, Math
        public string? TransformConfig { get; set; }
    }
}
