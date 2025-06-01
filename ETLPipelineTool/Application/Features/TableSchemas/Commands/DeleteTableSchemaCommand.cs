namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public record DeleteTableSchemaCommand(Guid Id) : IRequest<Unit>;
