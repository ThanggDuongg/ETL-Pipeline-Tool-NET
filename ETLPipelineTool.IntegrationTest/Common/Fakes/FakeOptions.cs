namespace ETLPipelineTool.IntegrationTest.Common.Fakes
{
    public class FakeOptions<T>(T config) : IOptions<T>
        where T : class, new()
    {
        public T Value => config;
    }
}
