using ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public record UpdateTableSchemaCommand(UpdateTableSchemaDto TableSchema) : IRequest<Unit>;
