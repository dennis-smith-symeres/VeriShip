using Telerik.Documents.Core.Fonts;
using Telerik.Windows.Documents.Core.Fonts;
using Telerik.Windows.Documents.Extensibility;

namespace Infrastructure.Services;

// The .NET Standard specification does not define APIs for getting specific fonts.
// PdfFormatProvider needs to have access to the font data so that it can read it and add it to the PDF file. 
// https://docs.telerik.com/devtools/document-processing/libraries/radwordsprocessing/formats-and-conversion/pdf/pdfformatprovider
internal class FontsProvider : FontsProviderBase
{
    private readonly string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

    public override byte[] GetFontData(FontProperties fontProperties)
    {
        string fontFamilyName = fontProperties.FontFamilyName;
        bool isBold = fontProperties.FontWeight == FontWeights.Bold;
        string fontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
        if (fontFamilyName == "Arial" && isBold)
        {
            return this.GetFontDataFromFontFolder("arialbd.ttf");
        }
        else if (fontFamilyName == "Arial")
        {
            return this.GetFontDataFromFontFolder("arial.ttf");
        }
        else if (fontFamilyName == "Calibri" && isBold)
        {
            return this.GetFontDataFromFontFolder("calibrib.ttf");
        }
        else if (fontFamilyName == "Calibri")
        {
            return this.GetFontDataFromFontFolder("calibri.ttf");
        }

        return null;
    }

    private byte[] GetFontDataFromFontFolder(string fontFileName)
    {
        using (FileStream fileStream = File.OpenRead(this.fontFolder + "\\" + fontFileName))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}