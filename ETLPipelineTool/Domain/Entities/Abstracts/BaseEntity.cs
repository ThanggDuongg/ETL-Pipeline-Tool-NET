namespace ETLPipelineTool.Domain.Entities.Abstracts
{
  public abstract class BaseEntity : IIdentity<Guid>, IVersioning, IAuditing
  {
    public Guid Id { get; protected set; }
    public byte[]? RowVersion { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = default!;
    public DateTime ModifiedOn { get; set; }

    protected BaseEntity()
    {
      Id = SequentialGuidGenerator.NewGuid();
    }
  }
}
