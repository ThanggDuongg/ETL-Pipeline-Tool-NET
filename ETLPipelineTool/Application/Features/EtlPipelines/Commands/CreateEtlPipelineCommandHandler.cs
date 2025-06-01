namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
  public class CreateEtlPipelineCommandHandler(
    IEtlContext context,
    IEtlPipelineRepository etlPipelineRepository
  ) : IRequestHandler<CreateEtlPipelineCommand, Guid>
  {
    public async Task<Guid> Handle(
      CreateEtlPipelineCommand command,
      CancellationToken cancellationToken
    )
    {
      var entity = EtlPipelineMapper.ToEntity(command);

      await etlPipelineRepository.AddAsync(entity, cancellationToken);
      await context.SaveChangesAsync(cancellationToken);

      return entity.Id;
    }
  }
}
