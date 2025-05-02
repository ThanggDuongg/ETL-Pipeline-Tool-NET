using ETLPipelineTool.Domain.Entities.Abstracts;

namespace ETLPipelineTool.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public string TableName { get; set; } = default!;
        public string ActionType { get; set; } = default!; // CREATE, UPDATE, DELETE
        public string KeyValues { get; set; } = default!; // JSON: { id: "xxx-xxx" }
        public string? OldValues { get; set; } // JSON
        public string? NewValues { get; set; } // JSON
    }
}
