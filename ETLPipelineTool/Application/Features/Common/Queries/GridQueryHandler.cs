using ETLPipelineTool.Application.Dtos.V1.Responses;

namespace ETLPipelineTool.Application.Features.Common.Queries
{
  public abstract class GridQueryHandler<TQuery, TEntity, TDto>
    : IRequestHandler<TQuery, GridResultDataDto<TDto>>
    where TQuery : GridQuery<GridResultDataDto<TDto>>
    where TEntity : BaseEntity
    where TDto : class
  {
    protected virtual List<string> ExpandColumns { get; set; } = [];

    public async Task<GridResultDataDto<TDto>> Handle(
      TQuery request,
      CancellationToken cancellationToken
    )
    {
      await PreHandleAsync(request, cancellationToken);

      var query = GetBaseQuery(request);

      query = ApplyFiltering(query, request);
      query = query.ApplySorting(request, x => x.SortFields);

      var total = await query.CountAsync(cancellationToken);

      List<TDto> data;
      if (request.PreloadAllData)
      {
        data = await query.Select(ProjectToDto(request)).ToListAsync(cancellationToken);
        data = await ProcessDataAsync(data, request, cancellationToken);

        await PostHandleAsync(data, request, cancellationToken);
        return new GridResultDataDto<TDto>(data, total);
      }

      data = await query
        .Skip(request.Skip)
        .Take(request.Take)
        .Select(ProjectToDto(request))
        .ToListAsync(cancellationToken);

      data = await ProcessDataAsync(data, request, cancellationToken);

      await PostHandleAsync(data, request, cancellationToken);
      return new GridResultDataDto<TDto>(data, total);
    }

    protected virtual Task PreHandleAsync(TQuery request, CancellationToken cancellationToken) =>
      Task.CompletedTask;

    protected virtual Task<List<TDto>> ProcessDataAsync(
      List<TDto> data,
      TQuery request,
      CancellationToken cancellationToken
    ) => Task.FromResult(data);

    protected virtual Task PostHandleAsync(
      List<TDto> data,
      TQuery request,
      CancellationToken cancellationToken
    ) => Task.CompletedTask;

    protected abstract IQueryable<TEntity> GetBaseQuery(TQuery request);

    protected virtual Expression<Func<TEntity, TDto>> ProjectToDto(TQuery request)
    {
      if (request.PreloadAllData && ExpandColumns.Count > 0)
      {
        var columns = request.SortFields.Select(x => x.Field).Concat(ExpandColumns).ToArray();
        return ProjectionHelper.CreateDynamicSelector<TEntity, TDto>(columns);
      }

      return BuildFullProjection();
    }

    protected abstract Expression<Func<TEntity, TDto>> BuildFullProjection();

    protected virtual IQueryable<TEntity> ApplyFiltering(
      IQueryable<TEntity> query,
      TQuery request
    ) => query;
  }
}
