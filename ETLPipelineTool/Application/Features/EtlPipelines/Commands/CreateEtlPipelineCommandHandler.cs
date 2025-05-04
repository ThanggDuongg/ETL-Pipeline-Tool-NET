namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
    public class CreateEtlPipelineCommandHandler(
        IEtlContext context,
        IEtlPipelineRepository etlPipelineRepository
    ) : IRequestHandler<CreateEtlPipelineCommand, Guid>
    {
        private readonly IEtlContext _context = context;
        private readonly IEtlPipelineRepository _etlPipelineRepository = etlPipelineRepository;

        public async Task<Guid> Handle(
            CreateEtlPipelineCommand command,
            CancellationToken cancellationToken
        )
        {
            var entity = EtlPipelineMapper.ToEntity(command);

            await _etlPipelineRepository.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
