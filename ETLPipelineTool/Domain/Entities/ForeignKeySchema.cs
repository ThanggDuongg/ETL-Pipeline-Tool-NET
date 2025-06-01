namespace ETLPipelineTool.Domain.Entities
{
  public class ForeignKeySchema : BaseEntity
  {
    public Guid TableSchemaId { get; set; }
    public TableSchema? TableSchema { get; set; }
    public string ConstraintName { get; set; } = default!;
    public string PrincipalTable { get; set; } = default!;
    public ICollection<ForeignKeyColumn> Columns { get; set; } = [];
    public ICollection<ForeignKeyPrincipalColumn> PrincipalColumns { get; set; } = [];
  }
}
