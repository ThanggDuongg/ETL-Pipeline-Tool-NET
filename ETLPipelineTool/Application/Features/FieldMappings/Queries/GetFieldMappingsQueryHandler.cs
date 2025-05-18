using ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Queries
{
    public class GetFieldMappingsQueryHandler(IFieldMappingRepository fieldMappingRepository)
        : GridQueryHandler<GetFieldMappingsQuery, FieldMapping, FieldMappingDataDto>
    {
        protected override List<string> ExpandColumns { get; set; } =
            [
                nameof(FieldMappingDataDto.Id),
                nameof(FieldMappingDataDto.RowVersion),
                $"{nameof(FieldMappingDataDto.EtlPipeline)}.{nameof(FieldMappingDataDto.EtlPipeline.Name)}",
            ];

        protected override IQueryable<FieldMapping> GetBaseQuery(GetFieldMappingsQuery request)
        {
            return fieldMappingRepository.GetList();
        }

        protected override IQueryable<FieldMapping> ApplyFiltering(
            IQueryable<FieldMapping> query,
            GetFieldMappingsQuery request
        )
        {
            return query;
        }

        protected override Expression<Func<FieldMapping, FieldMappingDataDto>> BuildFullProjection()
        {
            return FieldMappingProjection.AsFieldMappingDataDto();
        }
    }
}
