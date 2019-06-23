using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Quick.Common.Mvc.Csv
{
    public class CsvFileResult<TModel> : FileResult where TModel : class
    {
        public IEnumerable<TModel> Model { get; private set; }

        public CsvFileResult(IEnumerable<TModel> model, string fileName = null) : base("text/comma-separated-values")
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            fileName = string.IsNullOrWhiteSpace(fileName) ? Path.GetRandomFileName() : fileName;
            FileDownloadName = Path.ChangeExtension(fileName, "csv");
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            byte[] buffer = GenerateCsv();
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private byte[] GenerateCsv()
        {
            var csvWorksheet = new List<string>();

            var properties = typeof(TModel).GetProperties().Where(p => !p.IsDefined(typeof(CsvIgnoreAttribute)));

            var propertyDictionary = properties.Select(property => new { Property = property, CsvColumnAttribute = property.GetCustomAttribute<CsvColumnAttribute>() ?? new CsvColumnAttribute() })
                                               .OrderBy(o => o.CsvColumnAttribute.Sort)
                                               .ToDictionary(o => o.Property, o => o.CsvColumnAttribute);

            GenerateCsvHeader(csvWorksheet, propertyDictionary);

            GenerateCsvBody(csvWorksheet, propertyDictionary);

            FormatCsvSheet(csvWorksheet, propertyDictionary);

            var csvResult = string.Join(Environment.NewLine, csvWorksheet.ToArray());

            return Encoding.Default.GetBytes(csvResult);
        }

        private void FormatCsvSheet(List<string> csvWorksheet, Dictionary<PropertyInfo, CsvColumnAttribute> propertyDictionary)
        {

        }

        private void GenerateCsvHeader(List<string> csvWorksheet, Dictionary<PropertyInfo, CsvColumnAttribute> propertyDictionary)
        {
            var csvLine = new StringBuilder();

            PropertyInfo[] propertyInfoArray = propertyDictionary.Select(pd => pd.Key).ToArray();

            for (int col = 1; col <= propertyInfoArray.Length; col++)
            {
                PropertyInfo currentPropertyInfo = propertyInfoArray[col - 1];

                DisplayAttribute displayAttribute = currentPropertyInfo.GetCustomAttribute<DisplayAttribute>();

                DisplayNameAttribute displayNameAttribute = currentPropertyInfo.GetCustomAttribute<DisplayNameAttribute>();

                var displayName = displayAttribute?.GetName() ?? displayNameAttribute?.DisplayName ?? currentPropertyInfo.Name;

                if (col < propertyInfoArray.Length)
                    csvLine.Append(displayName).Append(",");
                else
                    csvLine.Append(displayName);
            }

            csvWorksheet.Add(csvLine.ToString());
        }

        private void GenerateCsvBody(List<string> csvWorksheet, Dictionary<PropertyInfo, CsvColumnAttribute> propertyDictionary)
        {
            var csvLines = new List<string>();

            TModel[] modelArray = Model.ToArray();
            PropertyInfo[] propertyInfoArray = propertyDictionary.Select(pd => pd.Key).ToArray();

            for (int row = 2; row <= modelArray.Length + 1; row++)
            {
                var csvLine = new StringBuilder();

                TModel currentModel = modelArray[row - 2];
                for (int col = 1; col <= propertyInfoArray.Length; col++)
                {
                    PropertyInfo currentPropertyInfo = propertyInfoArray[col - 1];
                    CsvColumnAttribute csvColumnAttribute = propertyDictionary[currentPropertyInfo];
                    var displayFormatAttribute = currentPropertyInfo.GetCustomAttribute<DisplayFormatAttribute>();
                    var cellValue = displayFormatAttribute != null && typeof(IFormattable).IsAssignableFrom(currentPropertyInfo.PropertyType) ?
                    ((IFormattable)currentPropertyInfo.GetValue(currentModel)).ToString(displayFormatAttribute.DataFormatString, CultureInfo.InvariantCulture)
                    : currentPropertyInfo.GetValue(currentModel);

                    if (col < propertyInfoArray.Length)
                        csvLine.Append(cellValue).Append(",");
                    else
                        csvLine.Append(cellValue);
                }

                csvLines.Add(csvLine.ToString());
            }

            csvWorksheet.AddRange(csvLines);
        }

    }
}