using ETLPipelineTool.Application.Dtos.V1.Requests.EtlPipelines;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Mappings
{
  public static class EtlPipelineMapper
  {
    public static GetEtlPipelinesQuery ToGetEtlPipelinesQuery(GridEtlPipelinesFilterDto dto)
    {
      return new GetEtlPipelinesQuery(
        dto.GridDataSourceDto.Take,
        dto.GridDataSourceDto.Skip,
        dto.GridDataSourceDto.PreloadAllData,
        dto.GridDataSourceDto.SortFields,
        dto.IsActive
      );
    }

    public static DeleteEtlPipelineCommand ToDeleteEtlPipelineCommand(Guid id)
    {
      return new DeleteEtlPipelineCommand(id);
    }

    public static UpdateEtlPipelineCommand ToUpdateEtlPipelineCommand(UpdateEtlPipelineDto dto)
    {
      return new UpdateEtlPipelineCommand(
        dto.Id,
        dto.Name,
        dto.Description,
        dto.SourceType,
        dto.TargetType,
        dto.SourceConfigurationJson,
        dto.TargetConfigurationJson,
        dto.IsActive,
        dto.RowVersion
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

    public static RunEtlPipelineCommand ToRunEtlPipelineCommand(Guid id)
    {
      return new RunEtlPipelineCommand(id);
    }
  }
}
