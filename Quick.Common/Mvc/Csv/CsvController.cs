using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Common.Mvc.Controllers;

namespace Quick.Common.Mvc.Csv
{
    public class CsvController : BaseController
    {
        public CsvFileResult<T> Excel<T>(IEnumerable<T> model) where T : class
        {
            return new CsvFileResult<T>(model);
        }

        public CsvFileResult<T> Excel<T>(IEnumerable<T> model, string fileName) where T : class
        {
            return new CsvFileResult<T>(model, fileName);
        }
    }
}