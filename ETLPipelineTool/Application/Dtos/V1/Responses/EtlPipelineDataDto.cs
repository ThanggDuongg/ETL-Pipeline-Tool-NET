namespace ETLPipelineTool.Application.Dtos.V1.Responses
{
    public record EtlPipelineDataDto(
        Guid Id,
        string Name,
        string Description,
        PipelineSourceType SourceType,
        PipelineTargetType TargetType,
        string SourceConfigurationJson,
        string TargetConfigurationJson,
        bool IsActive,
        byte[] RowVersion
    );
}
