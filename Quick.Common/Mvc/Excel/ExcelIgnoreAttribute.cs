using System;

namespace Quick.Common.Mvc.Excel
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ExcelIgnoreAttribute : Attribute
    {

    }
}
