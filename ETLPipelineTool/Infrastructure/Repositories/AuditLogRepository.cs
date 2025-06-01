using ETLPipelineTool.Domain.Entities;
using ETLPipelineTool.Domain.Interfaces.Repositories;
using ETLPipelineTool.Infrastructure.Persistence;

namespace ETLPipelineTool.Infrastructure.Repositories
{
  public class AuditLogRepository(IEtlContext etlContext)
    : BaseRepository<AuditLog>(etlContext),
      IAuditLogRepository { }
}
