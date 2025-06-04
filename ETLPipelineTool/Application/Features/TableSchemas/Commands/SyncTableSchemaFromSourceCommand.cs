namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class SyncTableSchemaFromSourceCommand(Guid EtlPipelineId, string TableName) : IRequest<Unit>
{
  public Guid EtlPipelineId { get; } = EtlPipelineId;
  public string TableName { get; } = TableName;
}
