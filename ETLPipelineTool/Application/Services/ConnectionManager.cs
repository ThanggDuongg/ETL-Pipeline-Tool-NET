using System.Collections.Concurrent;
using ETLPipelineTool.Application.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace ETLPipelineTool.Application.Services
{
  public class ConnectionManager(ILogger<ConnectionManager> logger)
    : IConnectionManager,
      IDisposable
  {
    private readonly ConcurrentDictionary<string, DbConnection> _connections = new();
    private bool _disposed;

    public async Task<DbConnection> GetConnectionAsync(
      string connectionString,
      CancellationToken cancellationToken = default
    )
    {
      ThrowIfDisposed();

      if (_connections.TryGetValue(connectionString, out var existingConnection))
      {
        return existingConnection;
      }

      logger.LogDebug("Creating new database connection");
      var connection = new SqlConnection(connectionString);
      await connection.OpenAsync(cancellationToken);

      _connections.TryAdd(connectionString, connection);

      return connection;
    }

    public async Task ReleaseAllConnectionsAsync()
    {
      ThrowIfDisposed();

      foreach (var connection in _connections.Values)
      {
        if (connection.State != System.Data.ConnectionState.Closed)
        {
          await connection.CloseAsync();
        }
      }
      _connections.Clear();
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
      {
        return;
      }

      if (disposing)
      {
        foreach (var connection in _connections.Values)
        {
          connection.Dispose();
        }
        _connections.Clear();
      }

      _disposed = true;
    }

    private void ThrowIfDisposed()
    {
      if (!_disposed)
      {
        return;
      }
      throw new ObjectDisposedException(nameof(ConnectionManager));
    }

    ~ConnectionManager()
    {
      Dispose(false);
    }
  }
}
