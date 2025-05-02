using ETLPipelineTool.Domain.Entities.Abstracts;

namespace ETLPipelineTool.Domain.Entities
{
    public class FieldMapping : BaseEntity
    {
        public Guid EtlPipelineId { get; set; }
        public EtlPipeline? EtlPipeline { get; set; }
        public int Order { get; set; } // Priority
        public string SourceField { get; set; } = default!;
        public string TargetField { get; set; } = default!;
        public string? TransformExpression { get; set; } // E.g. "value == null ? \"N/A\" : value.Trim()"
    }
}
