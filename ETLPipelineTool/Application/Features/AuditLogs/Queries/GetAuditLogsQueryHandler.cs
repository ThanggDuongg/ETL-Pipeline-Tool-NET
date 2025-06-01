using ETLPipelineTool.Application.Dtos.V1.Responses.AuditLogs;

namespace ETLPipelineTool.Application.Features.AuditLogs.Queries;

public class GetAuditLogsQueryHandler
  : GridQueryHandler<GetAuditLogsQuery, AuditLog, AuditLogDataDto>
{
  private readonly IAuditLogRepository _repository;

  public GetAuditLogsQueryHandler(IAuditLogRepository repository)
  {
    _repository = repository;
  }

  protected override List<string> ExpandColumns { get; set; } =
    [
      nameof(AuditLogDataDto.Id),
      nameof(AuditLogDataDto.TableName),
      nameof(AuditLogDataDto.ActionType),
      nameof(AuditLogDataDto.CreatedOn),
      nameof(AuditLogDataDto.CreatedBy),
    ];

  protected override IQueryable<AuditLog> GetBaseQuery(GetAuditLogsQuery request)
  {
    return _repository.GetList();
  }

  protected override IQueryable<AuditLog> ApplyFiltering(
    IQueryable<AuditLog> query,
    GetAuditLogsQuery request
  )
  {
    if (!string.IsNullOrEmpty(request.TableName))
    {
      query = query.Where(x => x.TableName.Contains(request.TableName));
    }

    if (!string.IsNullOrEmpty(request.ActionType))
    {
      query = query.Where(x => x.ActionType == request.ActionType);
    }

    if (request.CreatedOnFrom.HasValue)
    {
      query = query.Where(x => x.CreatedOn >= request.CreatedOnFrom.Value);
    }

    if (request.CreatedOnTo.HasValue)
    {
      query = query.Where(x => x.CreatedOn <= request.CreatedOnTo.Value);
    }

    return query;
  }

  protected override Expression<Func<AuditLog, AuditLogDataDto>> BuildFullProjection()
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
