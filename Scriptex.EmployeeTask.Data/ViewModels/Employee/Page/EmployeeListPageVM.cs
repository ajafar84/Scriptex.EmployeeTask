using PagedList;

namespace Scriptex.EmployeeTask.Data.ViewModels.Employee.Page
{
    public class EmployeeListPageVM
    {
        public IPagedList PagingMetaData { get; set; }
        public IPagedList<EmployeeGridItemVM> GridItemsVM { get; set; }
    }
}
