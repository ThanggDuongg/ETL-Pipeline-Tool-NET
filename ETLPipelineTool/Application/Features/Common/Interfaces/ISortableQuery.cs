namespace ETLPipelineTool.Application.Features.Common.Interfaces
{
    public interface ISortableQuery
    {
        ICollection<SortField> SortFields { get; set; }
    }
}
