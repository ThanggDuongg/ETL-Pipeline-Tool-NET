namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
    public class DeleteEtlPipelineCommand(Guid id) : IRequest<Unit>
    {
        public Guid Id { get; set; } = id;
    }
}
