using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;
using ETLPipelineTool.Application.Services;

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
    UpdateFieldMappingDto fieldMappingDto = command.FieldMapping;

    // Workaround: Force order/sequence
    FieldMappingOrderService.NormalizeSourceFieldOrders(fieldMappingDto.FieldMappingSources);
    FieldMappingOrderService.NormalizeTransformRuleSequences(fieldMappingDto.TransformRules);

    FieldMapping entity = await repository.GetByIdAsync(
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

    FieldMappingOrderService.NormalizeSourceFieldOrders(entity.SourceFields);
    FieldMappingOrderService.NormalizeTransformRuleSequences(entity.TransformRules);

    await context.SaveChangesAsync(cancellationToken);

    return Unit.Value;
  }

  private static void UpdateTransformRules(FieldMapping entity, UpdateFieldMappingDto dto)
  {
    HashSet<Guid?> updatedRuleIds = dto.TransformRules.Select(x => x.Id).ToHashSet();

    foreach (TransformRule? existing in entity.TransformRules.ToList())
    {
      if (!updatedRuleIds.Contains(existing.Id))
      {
        entity.TransformRules.Remove(existing);
      }
    }

    foreach (Dtos.V1.Requests.TransformRules.UpdateTransformRuleDto ruleDto in dto.TransformRules)
    {
      TransformRule? existing = entity.TransformRules.SingleOrDefault(x => x.Id == ruleDto.Id);
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
    HashSet<Guid?> updatedSourceIds = dto.FieldMappingSources.Select(x => x.Id).ToHashSet();

    foreach (FieldMappingSource? existing in entity.SourceFields.ToList())
    {
      if (!updatedSourceIds.Contains(existing.Id))
      {
        entity.SourceFields.Remove(existing);
      }
    }

    foreach (UpdateFieldMappingSourceDto sourceDto in dto.FieldMappingSources)
    {
      FieldMappingSource? existing = entity.SourceFields.SingleOrDefault(x => x.Id == sourceDto.Id);
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
