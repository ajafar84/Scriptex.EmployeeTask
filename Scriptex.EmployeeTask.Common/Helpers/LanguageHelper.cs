using Scriptex.EmployeeTask.Common.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace Scriptex.EmployeeTask.Common.Helpers
{
    public static class LanguageHelper
    {
        public static Expression<Func<TSource, string>> GetSourceExpression<TSource>()
        {
            string ar = CultureCode.ar.ToString();
            string en = CultureCode.en.ToString();
            string culture = CultureInfo.CurrentUICulture.Name;

            var parameterExpression = Expression.Parameter(typeof(TSource));
            var property = Expression.Property(parameterExpression,
                culture == ar ? "ArabicName" :
                culture == en ? "EnglishName" :
                "ArabicName");

            return Expression.Lambda<Func<TSource, string>>(property, parameterExpression);
        }

        public static Expression<Func<TSource, string>> GetSourceExpression<TSource>(string field)
        {
            string ar = CultureCode.ar.ToString();
            string en = CultureCode.en.ToString();
            string culture = CultureInfo.CurrentUICulture.Name;

            var parameterExpression = Expression.Parameter(typeof(TSource));
            var property = Expression.Property(parameterExpression,
                culture == ar ? $"{field}ArabicName" :
                culture == en ? $"{field}EnglishName" :
                $"{field}ArabicName");

            return Expression.Lambda<Func<TSource, string>>(property, parameterExpression);
        }

        public static Expression<Func<TSource, string>> GetSourceExpression<TSource>(string field, Dictionary<CultureCode, string> languageIdentifiers)
        {
            string ar = CultureCode.ar.ToString();
            string en = CultureCode.en.ToString();
            string culture = CultureInfo.CurrentUICulture.Name;

            var parameterExpression = Expression.Parameter(typeof(TSource));
            var property = Expression.Property(parameterExpression,
                culture == ar ? $"{field}{languageIdentifiers[CultureCode.ar]}" :
                culture == en ? $"{field}{languageIdentifiers[CultureCode.en]}" :
                $"{field}{languageIdentifiers[CultureCode.ar]}");

            return Expression.Lambda<Func<TSource, string>>(property, parameterExpression);
        }
    }
}
