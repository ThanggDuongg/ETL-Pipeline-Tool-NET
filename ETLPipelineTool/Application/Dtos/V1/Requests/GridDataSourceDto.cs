namespace ETLPipelineTool.Application.Dtos.V1.Requests
{
  public record GridDataSourceDto()
  {
    public int Take { get; init; } = 10;
    public int Skip { get; init; } = 0;
    public bool PreloadAllData { get; init; } = false;
    public ICollection<SortField> SortFields { get; init; } = [];
  }
}
