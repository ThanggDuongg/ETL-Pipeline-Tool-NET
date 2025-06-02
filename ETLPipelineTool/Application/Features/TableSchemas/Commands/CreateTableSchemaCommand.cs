using ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class CreateTableSchemaCommand(CreateTableSchemaDto TableSchema) : IRequest<Unit>
{
  public CreateTableSchemaDto TableSchema { get; } = TableSchema;
}
