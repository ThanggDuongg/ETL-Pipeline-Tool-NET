using ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Queries
{
  public class GetFieldMappingDetailQuery(Guid id) : IRequest<FieldMappingDataDto>
  {
    public Guid Id { get; set; } = id;
  }
}
