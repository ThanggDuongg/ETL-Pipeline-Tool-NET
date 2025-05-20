namespace ETLPipelineTool.Domain.Entities
{
    public class ForeignKeyPrincipalColumn : BaseEntity
    {
        public Guid ForeignKeySchemaId { get; set; }
        public ForeignKeySchema? ForeignKeySchema { get; set; }
        public string PrincipalColumnName { get; set; } = default!;
    }
}
