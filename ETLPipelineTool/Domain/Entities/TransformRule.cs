namespace ETLPipelineTool.Domain.Entities
{
    public class TransformRule : BaseEntity
    {
        public Guid EtlPipelineId { get; set; }
        public EtlPipeline? EtlPipeline { get; set; }
        public int Sequence { get; set; } // Priority of rules
        public TransformRuleType RuleType { get; set; }
        public string RuleConfigurationJson { get; set; } = default!; // Script/lookup configuration details
    }
}
