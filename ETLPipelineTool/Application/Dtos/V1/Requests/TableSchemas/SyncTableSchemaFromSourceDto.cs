namespace ETLPipelineTool.Application.Dtos.V1.Requests.TableSchemas;

public record SyncTableSchemaFromSourceDto(Guid EtlPipelineId, string TableName);
