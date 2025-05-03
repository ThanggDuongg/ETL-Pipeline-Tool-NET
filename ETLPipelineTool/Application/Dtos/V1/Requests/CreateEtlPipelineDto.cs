namespace ETLPipelineTool.Application.Dtos.V1.Requests
{
    public class CreateEtlPipelineDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public PipelineSourceType SourceType { get; set; }
        public PipelineTargetType TargetType { get; set; }
        public string SourceConfigurationJson { get; set; } = default!;
        public string TargetConfigurationJson { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
