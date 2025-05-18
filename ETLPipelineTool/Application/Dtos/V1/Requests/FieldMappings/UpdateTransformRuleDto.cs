namespace ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings
{
    public record UpdateTransformRuleDto(
        Guid? Id,
        int Sequence,
        TransformRuleType RuleType,
        string RuleConfigurationJson,
        byte[]? RowVersion
    );
}
