using ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules;

namespace ETLPipelineTool.Application.Features.TransformRules.Queries;

public record GetTransformRuleDetailQuery(Guid Id) : IRequest<TransformRuleDataDto>;
