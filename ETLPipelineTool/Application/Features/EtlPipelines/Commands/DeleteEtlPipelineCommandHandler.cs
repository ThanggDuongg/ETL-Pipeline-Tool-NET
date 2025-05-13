namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
    public class DeleteEtlPipelineCommandHandler(
        IEtlPipelineRepository repository,
        IEtlContext context
    ) : IRequestHandler<DeleteEtlPipelineCommand, Unit>
    {
        public async Task<Unit> Handle(
            DeleteEtlPipelineCommand command,
            CancellationToken cancellationToken
        )
        {
            await repository.DeleteByIdAsync(command.Id, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
