using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace ETLPipelineTool.Application.Services.Interfaces
{
  public interface IConnectionManager
  {
    Task<DbConnection> GetConnectionAsync(
      string connectionString,
      CancellationToken cancellationToken = default
    );

    Task ReleaseAllConnectionsAsync();
  }
}
