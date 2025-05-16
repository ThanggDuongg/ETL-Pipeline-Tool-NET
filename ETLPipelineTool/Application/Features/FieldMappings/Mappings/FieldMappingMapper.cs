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
                SourceFields = command.SourceFields,
                TargetField = command.TargetField,
                TransformRuleType = command.TransformRuleType,
                TransformConfig = command.TransformConfig,
            };
        }

        public static UpdateFieldMappingCommand ToUpdateFieldMappingCommand(
            UpdateFieldMappingDto dto
        )
        {
            return new UpdateFieldMappingCommand(
                dto.Id,
                dto.Order,
                dto.SourceFields,
                dto.TargetField,
                dto.TransformRuleType,
                dto.TransformConfig,
                dto.RowVersion
            );
        }
    }
}
