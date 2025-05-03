namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
    public class CreateEtlPipelineCommandHandler(IEtlContext context)
        : IRequestHandler<CreateEtlPipelineCommand, Guid>
    {
        private readonly IEtlContext _context = context;

        public async Task<Guid> Handle(
            CreateEtlPipelineCommand command,
            CancellationToken cancellationToken
        )
        {
            var entity = EtlPipelineMapper.ToEntity(command);

            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
