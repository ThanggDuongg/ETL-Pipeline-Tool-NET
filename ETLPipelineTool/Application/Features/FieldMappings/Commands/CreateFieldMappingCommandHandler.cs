using ETLPipelineTool.Application.Services;

namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
  public class CreateFieldMappingCommandHandler(
    IEtlContext context,
    IFieldMappingRepository fieldMappingRepository
  ) : IRequestHandler<CreateFieldMappingCommand, Guid>
  {
    public async Task<Guid> Handle(
      CreateFieldMappingCommand command,
      CancellationToken cancellationToken
    )
    {
      // Workaround: Force order/sequence
      FieldMappingOrderService.NormalizeSourceFieldOrders(command.FieldMapping.FieldMappingSources);
      FieldMappingOrderService.NormalizeTransformRuleSequences(command.FieldMapping.TransformRules);

      var entity = FieldMappingMapper.ToEntity(command);

      await fieldMappingRepository.AddAsync(entity, cancellationToken);
      await context.SaveChangesAsync(cancellationToken);

      return entity.Id;
    }
  }
}
