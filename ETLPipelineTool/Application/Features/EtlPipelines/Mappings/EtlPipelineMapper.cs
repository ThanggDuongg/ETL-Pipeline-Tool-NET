using ETLPipelineTool.Application.Dtos.V1.Requests;
using ETLPipelineTool.Application.Dtos.V1.Responses;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Mappings
{
    public static class EtlPipelineMapper
    {
        public static EtlPipelineDataDto ToEtlPipelineDataDto(EtlPipeline entity)
        {
            return new EtlPipelineDataDto(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.SourceType,
                entity.TargetType,
                entity.SourceConfigurationJson,
                entity.TargetConfigurationJson,
                entity.IsActive,
                entity.RowVersion
            );
        }

        public static GetEtlPipelineDetailQuery ToGetEtlPipelineDetailQuery(Guid id)
        {
            return new GetEtlPipelineDetailQuery(id);
        }

        public static CreateEtlPipelineCommand ToCreateEtlPipelineCommand(CreateEtlPipelineDto dto)
        {
            return new CreateEtlPipelineCommand(
                dto.Name,
                dto.Description,
                dto.SourceType,
                dto.TargetType,
                dto.SourceConfigurationJson,
                dto.TargetConfigurationJson,
                dto.IsActive
            );
        }

        public static EtlPipeline ToEntity(CreateEtlPipelineCommand command)
        {
            return new EtlPipeline
            {
                Name = command.Name,
                Description = command.Description,
                SourceType = command.SourceType,
                TargetType = command.TargetType,
                SourceConfigurationJson = command.SourceConfigurationJson,
                TargetConfigurationJson = command.TargetConfigurationJson,
                IsActive = command.IsActive,
            };
        }
    }
}
