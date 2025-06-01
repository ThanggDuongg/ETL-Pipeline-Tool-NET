namespace ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings
{
  public record UpdateFieldMappingSourceDto(
    Guid? Id,
    int Order,
    string SourceField,
    byte[]? RowVersion
  );
}
