using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
    public class UpdateFieldMappingCommand(UpdateFieldMappingDto fieldMapping) : IRequest<Unit>
    {
        public UpdateFieldMappingDto FieldMapping { get; set; } = fieldMapping;
    }
}
