using ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Queries;

public class GetTableSchemaDetailQuery(Guid Id) : IRequest<TableSchemaDataDto>
{
  public Guid Id { get; } = Id;
}
