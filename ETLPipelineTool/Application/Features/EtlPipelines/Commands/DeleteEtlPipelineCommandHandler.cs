namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
    public class DeleteEtlPipelineCommandHandler(
        IEtlPipelineRepository repository,
        IEtlContext context
    ) : IRequestHandler<DeleteEtlPipelineCommand, Unit>
    {
        private readonly IEtlPipelineRepository _repository = repository;
        private readonly IEtlContext _context = context;

        public async Task<Unit> Handle(
            DeleteEtlPipelineCommand command,
            CancellationToken cancellationToken
        )
        {
            await _repository.DeleteByIdAsync(command.Id, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
