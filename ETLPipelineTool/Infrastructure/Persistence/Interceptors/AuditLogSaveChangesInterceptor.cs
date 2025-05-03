namespace ETLPipelineTool.Infrastructure.Persistence.Interceptors
{
    public class AuditLogSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default
        )
        {
            if (eventData.Context is not EtlContext db)
            {
                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entries = db
                .ChangeTracker.Entries()
                .Where(e =>
                    e.Entity is not AuditLog
                    && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified
                        || e.State == EntityState.Deleted
                    )
                )
                .ToList();

            foreach (var entry in entries)
            {
                var now = DateTime.UtcNow;
                var user = "SYSTEM";
                if (entry.Entity is IAuditing auditing)
                {
                    now = auditing.CreatedOn;
                    user = auditing.CreatedBy;
                }

                db.AuditLogs.Add(
                    new AuditLog
                    {
                        TableName = entry.Metadata.GetTableName()!,
                        ActionType = entry.State.ToString().ToUpper(),
                        KeyValues = JsonSerializer.Serialize(
                            entry
                                .Properties.Where(p => p.Metadata.IsPrimaryKey())
                                .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)
                        ),
                        OldValues =
                            entry.State != EntityState.Added
                                ? JsonSerializer.Serialize(entry.OriginalValues.ToObject())
                                : null,
                        NewValues =
                            entry.State != EntityState.Deleted
                                ? JsonSerializer.Serialize(entry.CurrentValues.ToObject())
                                : null,
                        CreatedBy = user,
                        CreatedOn = now,
                        ModifiedBy = user,
                        ModifiedOn = now,
                    }
                );
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
