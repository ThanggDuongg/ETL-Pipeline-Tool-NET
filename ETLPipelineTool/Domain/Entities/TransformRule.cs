namespace ETLPipelineTool.Domain.Entities
{
    public class TransformRule : BaseEntity
    {
        public Guid FieldMappingId { get; set; }
        public FieldMapping? FieldMapping { get; set; }
        public int Sequence { get; set; } // Priority of rules
        public TransformRuleType RuleType { get; set; }
        public string RuleConfigurationJson { get; set; } = default!; // Script/lookup configuration details
    }
}
