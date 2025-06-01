namespace ETLPipelineTool.Application.Dtos.Sorting
{
  public record SortField(string Field, SortMode Mode = SortMode.Asc);
}
