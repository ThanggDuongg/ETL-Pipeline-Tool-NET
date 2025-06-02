using System;
using System.Linq.Expressions;
using ETLPipelineTool.Application.Dtos.V1.Responses.AuditLogs;
using ETLPipelineTool.Domain.Entities;

namespace ETLPipelineTool.Application.Features.AuditLogs.Projections
{
  public static class AuditLogProjection
  {
    public static Expression<Func<AuditLog, AuditLogDataDto>> AsAuditLogDataDto()
    {
      return entity => new AuditLogDataDto
      {
        Id = entity.Id,
        TableName = entity.TableName,
        ActionType = entity.ActionType,
        KeyValues = entity.KeyValues,
        OldValues = entity.OldValues,
        NewValues = entity.NewValues,
        CreatedBy = entity.CreatedBy,
        CreatedOn = entity.CreatedOn,
      };
    }
  }
}
