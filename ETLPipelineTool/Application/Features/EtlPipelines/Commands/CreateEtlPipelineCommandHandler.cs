namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
    public class CreateEtlPipelineCommandHandler(
        IEtlContext context,
        IEtlPipelineRepository etlPipelineRepository
    ) : IRequestHandler<CreateEtlPipelineCommand, Unit>
    {
        public async Task<Unit> Handle(
            CreateEtlPipelineCommand command,
            CancellationToken cancellationToken
        )
        {
            var entity = EtlPipelineMapper.ToEntity(command);

            await etlPipelineRepository.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
