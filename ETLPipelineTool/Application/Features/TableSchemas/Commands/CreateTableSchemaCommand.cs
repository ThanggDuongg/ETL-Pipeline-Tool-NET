using ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public record CreateTableSchemaCommand(CreateTableSchemaDto TableSchema) : IRequest<Unit>;
