namespace ETLPipelineTool.Application.Features.Common.Queries
{
  public class GridQuery<TResponse>(
    int take,
    int skip,
    bool preloadAllData,
    ICollection<SortField> sortFields
  ) : IRequest<TResponse>, ISortableQuery
  {
    public int Take { get; set; } = take;
    public int Skip { get; set; } = skip;
    public bool PreloadAllData { get; set; } = preloadAllData;
    public ICollection<SortField> SortFields { get; set; } = sortFields;
  }
}
