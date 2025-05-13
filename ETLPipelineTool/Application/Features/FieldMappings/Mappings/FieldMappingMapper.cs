using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Mappings
{
    public static class FieldMappingMapper
    {
        public static DeleteFieldMappingCommand ToDeleteFieldMappingCommand(Guid id)
        {
            return new DeleteFieldMappingCommand(id);
        }

        public static FieldMapping ToEntity(CreateFieldMappingCommand command)
        {
            return new FieldMapping
            {
                EtlPipelineId = command.EtlPipelineId,
                Order = command.Order,
                SourceField = command.SourceField,
                TargetField = command.TargetField,
                TransformExpression = command.TransformExpression,
            };
        }

        public static UpdateFieldMappingCommand ToUpdateFieldMappingCommand(
            UpdateFieldMappingDto dto
        )
        {
            return new UpdateFieldMappingCommand(
                dto.Id,
                dto.Order,
                dto.SourceField,
                dto.TargetField,
                dto.TransformExpression,
                dto.RowVersion
            );
        }
    }
}
