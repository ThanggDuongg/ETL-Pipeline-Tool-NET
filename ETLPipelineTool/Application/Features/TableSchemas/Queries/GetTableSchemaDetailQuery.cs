using ETLPipelineTool.Application.Dtos.V1.Responses.TableSchemas;

namespace ETLPipelineTool.Application.Features.TableSchemas.Queries;

public record GetTableSchemaDetailQuery(Guid Id) : IRequest<TableSchemaDataDto>;
