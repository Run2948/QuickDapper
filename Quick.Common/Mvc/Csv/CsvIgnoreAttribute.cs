using System;

namespace Quick.Common.Mvc.Csv
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class CsvIgnoreAttribute : Attribute
    {

    }
}
