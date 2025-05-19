//using ETLPipelineTool.Infrastructure.Transformers.Interfaces;

//namespace ETLPipelineTool.Infrastructure.Transformers
//{
//    public class ConcatTransformRule : ITransformRule
//    {
//        public Task<object?> ApplyAsync(
//            Dictionary<string, object?> columns,
//            List<string> sourceFields,
//            string? transformConfig,
//            CancellationToken cancellationToken = default
//        )
//        {
//            var result = string.Join(
//                "",
//                sourceFields.Select(f => columns.TryGetValue(f, out var v) ? v?.ToString() : "")
//            );
//            return Task.FromResult<object?>(result);
//        }
//    }
//}
