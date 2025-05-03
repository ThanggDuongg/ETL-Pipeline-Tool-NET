using ETLPipelineTool.Application.Dtos.V1.Requests;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Mappings
{
    public static class EtlPipelineMapper
    {
        public static CreateEtlPipelineCommand ToCommand(CreateEtlPipelineDto dto)
        {
            return new CreateEtlPipelineCommand
            {
                Name = dto.Name,
                Description = dto.Description,
                SourceType = dto.SourceType,
                TargetType = dto.TargetType,
                SourceConfigurationJson = dto.SourceConfigurationJson,
                TargetConfigurationJson = dto.TargetConfigurationJson,
                IsActive = dto.IsActive,
            };
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
