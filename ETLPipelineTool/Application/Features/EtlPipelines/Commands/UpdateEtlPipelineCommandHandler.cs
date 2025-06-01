namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
  public class UpdateEtlPipelineCommandHandler(
    IEtlPipelineRepository repository,
    IEtlContext context
  ) : IRequestHandler<UpdateEtlPipelineCommand, Unit>
  {
    public async Task<Unit> Handle(
      UpdateEtlPipelineCommand command,
      CancellationToken cancellationToken
    )
    {
      var entity = await repository.GetByIdAsync(command.Id, cancellationToken);

      entity.Name = command.Name;
      entity.Description = command.Description;
      entity.SourceType = command.SourceType;
      entity.TargetType = command.TargetType;
      entity.SourceConfigurationJson = command.SourceConfigurationJson;
      entity.TargetConfigurationJson = command.TargetConfigurationJson;
      entity.IsActive = command.IsActive;
      entity.RowVersion = command.RowVersion;

      await repository.UpdateAsync(entity, cancellationToken);
      await context.SaveChangesAsync(cancellationToken);
      return Unit.Value;
    }
  }
}
