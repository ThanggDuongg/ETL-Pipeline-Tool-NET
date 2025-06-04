namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record SyncTableSchemasFromSourceDto(Guid EtlPipelineId, ICollection<string>? TableNames);
