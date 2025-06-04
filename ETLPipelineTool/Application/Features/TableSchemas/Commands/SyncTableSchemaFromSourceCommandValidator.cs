namespace ETLPipelineTool.Application.Features.TableSchemas.Commands;

public class SyncTableSchemaFromSourceCommandValidator
  : AbstractValidator<SyncTableSchemaFromSourceCommand>
{
  public SyncTableSchemaFromSourceCommandValidator()
  {
    RuleFor(x => x.EtlPipelineId).NotEmpty();
    RuleFor(x => x.TableName).NotEmpty();
  }
}
