using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Commands;

public class UpdateFieldMappingCommandHandler(
  IFieldMappingRepository repository,
  IEtlContext context
) : IRequestHandler<UpdateFieldMappingCommand, Unit>
{
  public async Task<Unit> Handle(
    UpdateFieldMappingCommand command,
    CancellationToken cancellationToken
  )
  {
    var fieldMappingDto = command.FieldMapping;
    var entity = await repository.GetByIdAsync(
      fieldMappingDto.Id,
      null,
      f => f.Include(x => x.SourceFields).Include(x => x.TransformRules),
      true,
      cancellationToken
    );

    entity.Order = fieldMappingDto.Order;
    entity.TargetField = fieldMappingDto.TargetField;
    entity.RowVersion = fieldMappingDto.RowVersion;

    UpdateSourceFields(entity, fieldMappingDto);
    UpdateTransformRules(entity, fieldMappingDto);

    await context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }

  private static void UpdateTransformRules(FieldMapping entity, UpdateFieldMappingDto dto)
  {
    var updatedRuleIds = dto.TransformRules.Select(x => x.Id).ToHashSet();

    foreach (var existing in entity.TransformRules.ToList())
    {
      if (!updatedRuleIds.Contains(existing.Id))
      {
        entity.TransformRules.Remove(existing);
      }
    }

    foreach (var ruleDto in dto.TransformRules)
    {
      var existing = entity.TransformRules.SingleOrDefault(x => x.Id == ruleDto.Id);
      if (existing != null)
      {
        existing.Sequence = ruleDto.Sequence;
        existing.RuleType = ruleDto.RuleType;
        existing.RuleConfigurationJson = ruleDto.RuleConfigurationJson;
        existing.RowVersion = ruleDto.RowVersion;
      }
      else
      {
        entity.TransformRules.Add(
          new TransformRule
          {
            Sequence = ruleDto.Sequence,
            RuleType = ruleDto.RuleType,
            RuleConfigurationJson = ruleDto.RuleConfigurationJson,
            FieldMappingId = entity.Id,
          }
        );
      }
    }
  }

  private static void UpdateSourceFields(FieldMapping entity, UpdateFieldMappingDto dto)
  {
    var updatedSourceIds = dto.FieldMappingSources.Select(x => x.Id).ToHashSet();

    foreach (var existing in entity.SourceFields.ToList())
    {
      if (!updatedSourceIds.Contains(existing.Id))
      {
        entity.SourceFields.Remove(existing);
      }
    }

    foreach (var sourceDto in dto.FieldMappingSources)
    {
      var existing = entity.SourceFields.SingleOrDefault(x => x.Id == sourceDto.Id);
      if (existing != null)
      {
        existing.Order = sourceDto.Order;
        existing.SourceField = sourceDto.SourceField;
        existing.RowVersion = sourceDto.RowVersion;
      }
      else
      {
        entity.SourceFields.Add(
          new FieldMappingSource
          {
            Order = sourceDto.Order,
            SourceField = sourceDto.SourceField,
            FieldMappingId = entity.Id,
          }
        );
      }
    }
  }
}
