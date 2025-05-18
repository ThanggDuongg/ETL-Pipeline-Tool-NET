using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
    public class CreateFieldMappingCommand(CreateFieldMappingDto fieldMapping) : IRequest<Guid>
    {
        public CreateFieldMappingDto FieldMapping { get; set; } = fieldMapping;
    }
}
