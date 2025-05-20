namespace ETLPipelineTool.Domain.Entities
{
    public class ForeignKeyColumn : BaseEntity
    {
        public Guid ForeignKeySchemaId { get; set; }
        public ForeignKeySchema? ForeignKeySchema { get; set; }
        public string ColumnName { get; set; } = default!;
    }
}
