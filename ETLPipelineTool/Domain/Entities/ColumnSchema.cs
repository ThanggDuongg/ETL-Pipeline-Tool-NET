namespace ETLPipelineTool.Domain.Entities
{
  public class ColumnSchema : BaseEntity
  {
    public Guid TableSchemaId { get; set; }
    public TableSchema? TableSchema { get; set; }
    public string ColumnName { get; set; } = default!;
    public string DataType { get; set; } = default!;
    public bool IsPrimaryKey { get; set; }
    public bool IsNullable { get; set; }
    public int? MaxLength { get; set; }
  }
}
