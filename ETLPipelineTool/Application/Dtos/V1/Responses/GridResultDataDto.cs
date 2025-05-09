namespace ETLPipelineTool.Application.Dtos.V1.Responses
{
    public record GridResultDataDto<T>(ICollection<T> Data, int Total);
}
