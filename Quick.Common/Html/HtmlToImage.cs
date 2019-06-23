using System.Drawing.Imaging;

namespace Quick.Common.Html
{
    public class HtmlToImage
    {
        public static byte[] ConvertHtmlToImage(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
                return htmlToImageConv.GenerateImageFromFile(url, ImageFormat.Jpeg.ToString());
            }
            else
                return null;
        }
    }
}
