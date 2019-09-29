using Scriptex.EmployeeTask.Common.Interfaces;

namespace Scriptex.EmployeeTask.Common.Classes.Search
{
    public class SearchField : ISearchField
    {
        public string FieldName { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
