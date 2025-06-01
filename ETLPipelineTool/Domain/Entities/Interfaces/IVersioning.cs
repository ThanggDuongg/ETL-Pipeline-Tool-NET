namespace ETLPipelineTool.Domain.Entities.Interfaces
{
  public interface IVersioning
  {
    byte[]? RowVersion { get; set; }
  }
}
