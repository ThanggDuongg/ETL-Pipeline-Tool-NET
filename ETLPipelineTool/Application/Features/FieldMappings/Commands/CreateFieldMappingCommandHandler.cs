using ETLPipelineTool.Application.Features.FieldMappings.Mappings;

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
            var entity = FieldMappingMapper.ToEntity(command);

            await fieldMappingRepository.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
