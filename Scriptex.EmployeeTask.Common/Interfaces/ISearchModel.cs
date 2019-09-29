namespace Scriptex.EmployeeTask.Common.Interfaces
{
    public interface ISearchModel
    {
        string OrderBy { get; set; }
        string OrderType { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
