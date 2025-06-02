using ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class UpdateTableSchemaCommand(UpdateTableSchemaDto TableSchema) : IRequest<Unit>
{
  public UpdateTableSchemaDto TableSchema { get; } = TableSchema;
}
