using Scriptex.EmployeeTask.Common.Classes.Search;
using Scriptex.EmployeeTask.Common.Interfaces;
using System.Collections.Generic;

namespace Scriptex.EmployeeTask.Common.Search
{
    public class SearchModel : ISearchModel
    {
        public List<SearchField> SearchFields { get; set; } = new List<SearchField>();
        public string OrderBy { get; set; }
        public string OrderType { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
