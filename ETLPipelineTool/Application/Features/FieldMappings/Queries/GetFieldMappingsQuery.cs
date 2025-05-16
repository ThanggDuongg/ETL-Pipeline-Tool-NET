using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Queries
{
    public class GetFieldMappingsQuery(
        int take,
        int skip,
        bool preloadAllData,
        ICollection<SortField> sortFields
    )
        : GridQuery<GridResultDataDto<FieldMappingDataDto>>(
            take,
            skip,
            preloadAllData,
            sortFields
        ) { }
}
