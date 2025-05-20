namespace ETLPipelineTool.Domain.Entities
{
    public class TableSchema : BaseEntity
    {
        public Guid EtlPipelineId { get; set; }
        public EtlPipeline? EtlPipeline { get; set; }
        public string TableName { get; set; } = default!;
        public ICollection<ColumnSchema> Columns { get; set; } = [];
        public ICollection<ForeignKeySchema> ForeignKeys { get; set; } = [];
    }
}
