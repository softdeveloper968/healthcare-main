using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Font;
using System.IO;

namespace Services.Helper
{
    public static class HtmlToByte
    {
        public static byte[] ConvertToPDFBytes(string html, bool isLandscape = false)
        {
            using (var outputStream = new MemoryStream())
            {
                var properties = new ConverterProperties();
                var writer = new PdfWriter(outputStream);
                var pdf = new PdfDocument(writer);
                var pageSize = new PageSize(PageSize.A4);
                if (isLandscape)
                {
                    pageSize = pageSize.Rotate();
                }
                pdf.SetDefaultPageSize(pageSize);
                FontProvider fontProvider = new DefaultFontProvider(true, true, true);
                properties.SetFontProvider(fontProvider);
                var document = HtmlConverter.ConvertToDocument(html, pdf, properties);
                document.Close();
                return outputStream.ToArray();
            }
        }
    }
}
