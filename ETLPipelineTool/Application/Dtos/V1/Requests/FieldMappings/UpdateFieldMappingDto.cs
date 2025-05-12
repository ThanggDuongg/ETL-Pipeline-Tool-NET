namespace ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;

public record UpdateFieldMappingDto(
    Guid Id,
    int Order,
    string SourceField,
    string TargetField,
    string? TransformExpression,
    byte[] RowVersion
);
