using ETLPipelineTool.Application.Dtos.V1.Requests.EtlExecutionLogs;
using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;
using ETLPipelineTool.Application.Features.EtlExecutionLogs.Mappings;
using ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

namespace ETLPipelineTool.Api.Controllers.V1
{
  [ApiVersion(1.0)]
  public class EtlExecutionLogController(IMediator mediator) : BaseApiController
  {
    [HttpPost("grid")]
    public async Task<GridResultDataDto<EtlExecutionLogDataDto>> GetGrid(
      [Required] [FromBody] GridEtlExecutionLogsFilterDto dto,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(
        EtlExecutionLogMapper.ToGetEtlExecutionLogsQuery(dto),
        cancellationToken
      );
    }

    [HttpGet("{id}")]
    public async Task<EtlExecutionLogDataDto> GetById(
      [FromRoute] Guid id,
      CancellationToken cancellationToken
    )
    {
      return await mediator.Send(new GetEtlExecutionLogDetailQuery(id), cancellationToken);
    }

    [HttpGet("pipeline/{pipelineId}/status-stream")]
    public async Task StreamPipelineStatus(
      [FromRoute] Guid pipelineId,
      CancellationToken cancellationToken
    )
    {
      Response.Headers.ContentType = "text/event-stream";
      Response.Headers.CacheControl = "no-cache";
      Response.Headers.Connection = "keep-alive";

      var pollingDelayMs = 2000;
      var maxPollingTime = TimeSpan.FromMinutes(30);
      var startTime = DateTime.UtcNow;
      var completed = false;

      while (
        !completed
        && DateTime.UtcNow - startTime < maxPollingTime
        && !cancellationToken.IsCancellationRequested
      )
      {
        try
        {
          var query = new GetEtlExecutionLogsQuery(
            take: 1,
            skip: 0,
            preloadAllData: false,
            sortFields: [new(nameof(EtlExecutionLogDataDto.StartTime), SortMode.Desc)],
            etlPipelineId: pipelineId
          );

          var result = await mediator.Send(query, cancellationToken);
          var latestLog = result.Data.FirstOrDefault();

          if (latestLog == null)
          {
            await WriteEventAsync("error", $"No execution logs found for pipeline {pipelineId}");
            break;
          }

          await WriteEventAsync(
            "status",
            JsonSerializer.Serialize(latestLog, JsonSetting.DefaultOptions)
          );

          completed = latestLog.Status != EtlExecutionStatus.Running;

          if (!completed)
          {
            await Task.Delay(pollingDelayMs, cancellationToken);
          }
        }
        catch (Exception ex)
        {
          await WriteEventAsync("error", ex.Message);
          break;
        }
      }

      await WriteEventAsync("complete", "Stream closed");
    }

    private async Task WriteEventAsync(string eventType, string data)
    {
      await Response.WriteAsync($"event: {eventType}\n");
      await Response.WriteAsync($"data: {data}\n\n");
      await Response.Body.FlushAsync();
    }
  }
}
