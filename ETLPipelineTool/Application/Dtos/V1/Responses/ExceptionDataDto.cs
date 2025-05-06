namespace ETLPipelineTool.Application.Dtos.V1.Responses
{
    public record ExceptionDataDto(string Message, IDictionary<string, string[]>? Errors = null) { }
}
