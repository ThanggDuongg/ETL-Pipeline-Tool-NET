namespace ETLPipelineTool.Domain.Entities
{
    public class FieldMappingSource : BaseEntity
    {
        public Guid FieldMappingId { get; set; }
        public FieldMapping? FieldMapping { get; set; }
        public int Order { get; set; }
        public string SourceField { get; set; } = default!;
    }
}
