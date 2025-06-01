namespace ETLPipelineTool.Domain.Entities
{
  public class FieldMapping : BaseEntity
  {
    public Guid EtlPipelineId { get; set; }
    public EtlPipeline? EtlPipeline { get; set; }
    public int Order { get; set; } // Priority
    public ICollection<FieldMappingSource> SourceFields { get; set; } = [];
    public string TargetField { get; set; } = default!;
    public ICollection<TransformRule> TransformRules { get; set; } = [];
  }
}
