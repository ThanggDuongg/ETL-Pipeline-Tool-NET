namespace ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;

public record UpdateFieldMappingDto(
    Guid Id,
    int Order,
    ICollection<string> SourceFields,
    string TargetField,
    string TransformRuleType,
    string? TransformConfig,
    byte[] RowVersion
);
