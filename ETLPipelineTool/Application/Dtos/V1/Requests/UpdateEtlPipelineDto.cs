namespace ETLPipelineTool.Application.Dtos.V1.Requests
{
    public record UpdateEtlPipelineDto(
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
