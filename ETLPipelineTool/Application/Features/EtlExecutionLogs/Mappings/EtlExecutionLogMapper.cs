using ETLPipelineTool.Application.Dtos.V1.Requests.EtlExecutionLogs;
using ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Mappings
{
  public static class EtlExecutionLogMapper
  {
    public static GetEtlExecutionLogsQuery ToGetEtlExecutionLogsQuery(
      GridEtlExecutionLogsFilterDto dto
    )
    {
      return new GetEtlExecutionLogsQuery(
        dto.GridDataSourceDto.Take,
        dto.GridDataSourceDto.Skip,
        dto.GridDataSourceDto.PreloadAllData,
        dto.GridDataSourceDto.SortFields,
        dto.EtlPipelineId,
        dto.Status
      );
    }
  }
}
