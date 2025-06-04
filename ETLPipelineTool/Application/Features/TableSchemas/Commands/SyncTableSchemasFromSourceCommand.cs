namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class SyncTableSchemasFromSourceCommand(
  Guid etlPipelineId,
  ICollection<string>? tableNames = null
) : IRequest<Unit>
{
  public Guid EtlPipelineId { get; } = etlPipelineId;
  public ICollection<string>? TableNames { get; } = tableNames;
}
