using Scriptex.EmployeeTask.Common.Classes.Search;
using Scriptex.EmployeeTask.Common.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;

namespace Scriptex.EmployeeTask.Common.Helpers
{
    public static class Linq
    {

        public static List<string> OrderTypes = new List<string>() { "desc", "asc" };

        // operators
        public static Dictionary<string, string> Opertors = new Dictionary<string, string>
        {
                {"GreaterThan",">" },
                {"GreaterThanOrEqual",">=" },
                {"LessThan","<" },
                {"LessThanOrEqual","<=" },
                {"Contain","like" },
                {"NotContain","notlike" },
                {"Equal","==" },
                {"NotEqual","!=" },
        };

        public static IQueryable<T> DynamicSearch<T>(this IQueryable<T> source, SearchModel searchModel)
        {
            List<PropertyInfo> tProperties = typeof(T).GetProperties().ToList();
            if (searchModel.SearchFields != null)
            {
                foreach (SearchField field in searchModel.SearchFields)
                {
                    if (!tProperties.Exists(p => p.Name == field.FieldName))
                    {
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(field.Value) && Opertors.Keys.Contains(field.Operator))
                    {
                        Type actualType = tProperties.Where(p => p.Name == field.FieldName).First().PropertyType;
                        Type underlyingType = Nullable.GetUnderlyingType(actualType) ?? actualType;

                        switch (field.Operator)
                        {
                            case "Contain":
                                try
                                {
                                    // nullable fields
                                    if (Nullable.GetUnderlyingType(actualType) != null)
                                    {
                                        source = source.Where($"{field.FieldName}.Value.ToString().Trim().Contains(@0)", field.Value.Trim());
                                    }
                                    else
                                    {
                                        source = source.Where($"{field.FieldName}.ToString().Trim().Contains(@0)", field.Value.Trim());
                                    }
                                }
                                catch (Exception) { continue; }
                                break;
                            case "NotContain":
                                try
                                {
                                    // nullable fields
                                    if (Nullable.GetUnderlyingType(actualType) != null)
                                    {
                                        source = source.Where($"!{field.FieldName}.Value.ToString().Trim().Contains(@0)", field.Value.Trim());
                                    }
                                    else
                                    {
                                        source = source.Where($"!{field.FieldName}.ToString().Trim().Contains(@0)", field.Value.Trim());
                                    }
                                }
                                catch (Exception) { continue; }
                                break;
                            default:
                                try
                                {
                                    object safeValue;
                                    // case on timespan
                                    if (actualType == typeof(TimeSpan) || actualType == typeof(TimeSpan?))
                                    {
                                        safeValue = TimeSpan.Parse(field.Value);
                                    }
                                    else
                                    {
                                        safeValue = (field.Value == null) ? null : Convert.ChangeType(field.Value, underlyingType);
                                    }
                                    source = source.Where($"{field.FieldName} {Opertors[field.Operator]} @0", safeValue);
                                }
                                catch (Exception) { continue; }
                                break;
                        }

                    }
                }
            }
            searchModel.OrderType = OrderTypes.Contains(searchModel.OrderType) && searchModel.OrderBy != null ? searchModel.OrderType : "desc";
            searchModel.OrderBy = tProperties.Exists(p => p.Name == searchModel.OrderBy) ? searchModel.OrderBy : tProperties.First().Name;
            source = source.OrderBy($"{searchModel.OrderBy} {searchModel.OrderType}");
            Debug.WriteLine(source);
            return source;
        }


        //public static IPagedList<T> ToCustomPagedList<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        //{
        //    IPagedList<T> pageList = superset.ToPagedList(pageNumber, pageSize);
        //    IPagedList actualMetaData = pageList.GetMetaData();
        //    return actualMetaData.PageNumber > actualMetaData.PageCount ? superset.ToPagedList(1, pageSize) : pageList;
        //}
    }
}
