using System;
using System.IO;
using System.Threading;

namespace Quick.Common.Html
{
    public class HtmlToPdf : IHtmlToPdf
    {
        public bool ConvertHtmlToPdf(string url, string storePath)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(storePath))
            {
                return false;
            }
            var pdfGenerator = new NReco.PdfGenerator.HtmlToPdfConverter();
            pdfGenerator.GeneratePdfFromFile(url, null, storePath);
            return true;
        }

        public byte[] ConvertHtmlToPdf(string htmlContent)
        {
            var pdfGenerator = new NReco.PdfGenerator.HtmlToPdfConverter();
            return pdfGenerator.GeneratePdf(htmlContent);
        }

        public byte[] ConvertHtmlToPdfFromUrl(string url)
        {
            var pdfGenerator = new NReco.PdfGenerator.HtmlToPdfConverter();
            return pdfGenerator.GeneratePdfFromFile(url, null);
        }
    }
    public interface IHtmlToPdf
    {
        bool ConvertHtmlToPdf(string url, string storePath);
        byte[] ConvertHtmlToPdf(string htmlContent);
        byte[] ConvertHtmlToPdfFromUrl(string url);
    }
}