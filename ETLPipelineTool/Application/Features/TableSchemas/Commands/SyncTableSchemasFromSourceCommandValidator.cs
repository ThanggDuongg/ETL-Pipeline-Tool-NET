namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class SyncTableSchemasFromSourceCommandValidator
  : AbstractValidator<SyncTableSchemasFromSourceCommand>
{
  public SyncTableSchemasFromSourceCommandValidator()
  {
    RuleFor(x => x.EtlPipelineId).NotEmpty();
  }
}
