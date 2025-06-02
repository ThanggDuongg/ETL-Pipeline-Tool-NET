using ETLPipelineTool.Application.Dtos.V1.Requests.AuditLogs;
using ETLPipelineTool.Application.Features.AuditLogs.Queries;

namespace ETLPipelineTool.Application.Features.AuditLogs.Mappings;

public static class AuditLogMapper
{
  public static GetAuditLogsQuery ToGetAuditLogsQuery(GridAuditLogsFilterDto dto)
  {
    return new GetAuditLogsQuery(
      dto.GridDataSourceDto.Take,
      dto.GridDataSourceDto.Skip,
      dto.GridDataSourceDto.PreloadAllData,
      dto.GridDataSourceDto.SortFields,
      dto.TableName,
      dto.ActionType,
      dto.CreatedOnFrom,
      dto.CreatedOnTo
    );
  }
}
