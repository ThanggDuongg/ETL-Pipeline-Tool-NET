using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;
using ETLPipelineTool.Application.Features.FieldMappings.Queries;

namespace ETLPipelineTool.Application.Features.FieldMappings.Mappings
{
    public static class FieldMappingMapper
    {
        public static GetFieldMappingsQuery ToGetFieldMappingsQuery(GridFieldMappingsFilterDto dto)
        {
            return new GetFieldMappingsQuery(
                dto.GridDataSourceDto.Take,
                dto.GridDataSourceDto.Skip,
                dto.GridDataSourceDto.PreloadAllData,
                dto.GridDataSourceDto.SortFields
            );
        }

        public static GetFieldMappingDetailQuery ToGetFieldMappingDetailQuery(Guid id)
        {
            return new GetFieldMappingDetailQuery(id);
        }

        public static DeleteFieldMappingCommand ToDeleteFieldMappingCommand(Guid id)
        {
            return new DeleteFieldMappingCommand(id);
        }

        public static FieldMapping ToEntity(CreateFieldMappingCommand command)
        {
            var dto = command.FieldMapping;
            return new FieldMapping
            {
                EtlPipelineId = dto.EtlPipelineId,
                Order = dto.Order,
                SourceFields =
                [
                    .. dto.FieldMappingSources.Select(x => new FieldMappingSource()
                    {
                        Order = x.Order,
                        SourceField = x.SourceField,
                    }),
                ],
                TargetField = dto.TargetField,
                TransformRules =
                [
                    .. dto.TransformRules.Select(x => new TransformRule
                    {
                        Sequence = x.Sequence,
                        RuleType = x.RuleType,
                        RuleConfigurationJson = x.RuleConfigurationJson,
                    }),
                ],
            };
        }

        public static UpdateFieldMappingCommand ToUpdateFieldMappingCommand(
            UpdateFieldMappingDto dto
        )
        {
            return new UpdateFieldMappingCommand(dto);
        }

        public static CreateFieldMappingCommand ToCreateFieldMappingCommand(
            CreateFieldMappingDto dto
        )
        {
            return new CreateFieldMappingCommand(dto);
        }
    }
}
