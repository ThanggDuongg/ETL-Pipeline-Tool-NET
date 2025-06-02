using ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Queries
{
  public class GetFieldMappingDetailQueryHandler(IFieldMappingRepository fieldMappingRepository)
    : IRequestHandler<GetFieldMappingDetailQuery, FieldMappingDataDto>
  {
    public async Task<FieldMappingDataDto> Handle(
      GetFieldMappingDetailQuery request,
      CancellationToken cancellationToken
    )
    {
      return await fieldMappingRepository.GetByIdAsync(
        request.Id,
        null,
        FieldMappingProjection.AsFieldMappingDataDto(),
        cancellationToken
      );
    }
  }
}
