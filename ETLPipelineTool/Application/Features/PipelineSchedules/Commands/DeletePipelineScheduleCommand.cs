namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public record DeletePipelineScheduleCommand(Guid Id) : IRequest<Unit>;
