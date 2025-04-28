namespace ETLPipelineTool.Domain.Entities.Interfaces
{
    public interface IIdentity<T>
    {
        T Id { get; }
    }
}
