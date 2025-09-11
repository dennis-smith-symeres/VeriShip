using Infrastructure.Services;
using SkiaSharp;
using Svg.Skia;
using Telerik.Documents.ImageUtils;
using Telerik.Documents.Media;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf;
using Telerik.Windows.Documents.Flow.FormatProviders.Pdf.Export;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Flow.Model.Editing;
using Telerik.Windows.Documents.Flow.Model.Shapes;
using Telerik.Windows.Documents.Flow.Model.Styles;
using Telerik.Windows.Documents.Flow.Model.Watermarks;
using Telerik.Windows.Documents.Media;
using Telerik.Windows.Documents.Model.Drawing.Shapes;
using VeriShip.Infrastructure.Services.Model.WordProcessor;

namespace VeriShip.Infrastructure.Services;

public class WordProcessor
{
    private readonly Options _options;

    private RadFlowDocument _document;


    private WordProcessor(RadFlowDocument document)
    {
        var fontsProvider = new FontsProvider();
        FixedExtensibilityManager.FontsProvider = fontsProvider;
        var jpegImageConverter = new JpegImageConverter();
        FixedExtensibilityManager.JpegImageConverter = jpegImageConverter;
        _document = document;
        _options = new Options();
    }

    private WordProcessor(Options options, RadFlowDocument document)
    {
        var fontsProvider = new FontsProvider();
        FixedExtensibilityManager.FontsProvider = fontsProvider;
        var jpegImageConverter = new JpegImageConverter();
        FixedExtensibilityManager.JpegImageConverter = jpegImageConverter;
        _options = options;
        _document = document;
    }

    public static WordProcessor Create(Options options, MergeCoA fields)
    {
        using var stream = File.OpenRead(options.PathTemplate);
        var templateDocument = new DocxFormatProvider().Import(stream);
       
        var list = new List<MergeCoA>([fields]);
        var mergedDocument = templateDocument.MailMerge(list);

        return new WordProcessor(options, mergedDocument);
    }


    public static WordProcessor Create<T>(Options options, T fields) where T : class, new()
    {
        using var stream = File.OpenRead(options.PathTemplate);
        var templateDocument = new DocxFormatProvider().Import(stream);

        var list = new List<T>([fields]);
        var mergedDocument = templateDocument.MailMerge(list);

        return new WordProcessor(options, mergedDocument);
    }

    public static WordProcessor Open(string filePath)
    {
        using var stream = File.OpenRead(filePath);
        var document = new DocxFormatProvider().Import(stream);
        return new WordProcessor(document);
    }

    public WordProcessor PrettifyTables()
    {
        var tables = _document.EnumerateChildrenOfType<Table>();

        foreach (var table in tables.ToList())
        {
            if (table.Rows.Count < 2 || table.Rows[0].Cells.Count < 3)
            {
                continue;
            }

            var firstCellText = "";
            var firstRowSpan = 1;
            var secondCellText = "";
            var secondRowSpan = 1;
            var mergedRow = table.Rows.FirstOrDefault();
            foreach (var tableRow in table.Rows)
            {
                var runs = tableRow.Cells[0].Blocks.FirstOrDefault()?.EnumerateChildrenOfType<Run>();
                var secondRuns = tableRow.Cells[1].Blocks.FirstOrDefault()?.EnumerateChildrenOfType<Run>();
                var secondNextText = string.Join("", secondRuns.Select(x => x.Text));
                if (runs != null)
                {
                    var nextText = string.Join("", runs.Select(x => x.Text));
                    if (nextText == firstCellText)
                    {
                        ReplaceRunsText("", runs);
                        firstRowSpan++;
                        var cell = tableRow.Cells[0];
                        if (cell != null)
                        {
                          
                            tableRow.Cells.Remove(tableRow.Cells[0]);
                            mergedRow.Cells[0].RowSpan = firstRowSpan;
                            if (secondRuns != null)
                            {
                                
                                if (secondNextText == secondCellText)
                                {
                                    ReplaceRunsText("", secondRuns);
                                    secondRowSpan++;
                                    var secondCell = tableRow.Cells[0];
                                    if (secondCell != null)
                                    {
                                        tableRow.Cells.Remove(tableRow.Cells[0]);
                                        mergedRow.Cells[1].RowSpan = secondRowSpan;
                                    }
                                    
                                }
                                else
                                {
                                    secondCellText = secondNextText;
                                    secondRowSpan = 1;
                                }
                            }
                            
                            
                        }
                        
                    }
                    else
                    {
                        firstCellText = nextText;
                        secondCellText = secondNextText;
                        secondRowSpan = 1;
                        mergedRow = tableRow;
                        firstRowSpan = 1;
                 
                    }
                }
            }
            
        }

        return this;
    }

    public WordProcessor ReplaceTexts(Dictionary<string, string> replacements)
    {
        var radFlowDocumentEditor = new RadFlowDocumentEditor(_document);
        foreach (var (key, value) in replacements)
        {
            radFlowDocumentEditor.ReplaceText(key, value, matchCase: true, matchWholeWord: true);
        }

        return this;
    }

    public RadFlowDocument GetRadFlowDocument() => _document;

    public WordProcessor AddDocument(RadFlowDocument document)
    {
        var options = new InsertDocumentOptions();
        options.ConflictingStylesResolutionMode = ConflictingStylesResolutionMode.UseTargetStyle;
        options.InsertLastParagraphMarker = true;

        var editor = new RadFlowDocumentEditor(_document);

        editor.InsertDocument(document, options);

        return this;
    }

  
    public WordProcessor AddStamp(byte[] imageBytes)
    {
        var stampImageInline = _document.EnumerateChildrenOfType<ImageInline>()
            .FirstOrDefault(x => x.Image.Description == "stamp");
        if (stampImageInline == null)
        {
            return this;
        }

        using (var image = SKBitmap.Decode(imageBytes))
        {
            var imageWidth = image.Width;
            var imageHeight = image.Height;

            var maxWidth = stampImageInline.Image.Width;
            var maxHeight = stampImageInline.Image.Height;

            var width = (imageWidth * maxHeight) / imageHeight;
            var height = maxHeight;
            if (width > maxWidth)
            {
                width = maxWidth;
                height = (imageHeight * maxWidth) / imageWidth;
            }

            var wordImage = stampImageInline.Image;
            wordImage.ImageSource = new ImageSource(imageBytes, "png");
            wordImage.Height = height;
            wordImage.Width = width;
        }

        return this;
    }

    private TableCell? FindParentTableCell(DocumentElementBase element)
    {
        var current = element;
        while (current != null)
        {
            if (current is TableCell cell)
            {
                return cell;
            }
            current = current.Parent;
        }
        return null;
    }
    
    private void RemoveSvgInline()
    {
        var structureImageInline = _document.EnumerateChildrenOfType<ImageInline>()
            .FirstOrDefault(x => x.Image.Description == "structure");
        
        if (structureImageInline?.Paragraph == null) return;
        
        var paragraph = structureImageInline.Paragraph;
        paragraph.Inlines.Remove(structureImageInline);
        var tableCell = FindParentTableCell(paragraph.Parent);

        tableCell?.Blocks.Clear();

    }
    
    private void RemoveSvgFloating()
    {
        var structureImageInline = _document.EnumerateChildrenOfType<FloatingImage>()
            .FirstOrDefault(x => x.Image.Description == "structure");
        if (structureImageInline?.Paragraph == null) return;
        var paragraph = structureImageInline.Paragraph;
        paragraph.Inlines.Remove(structureImageInline);

    }
    public WordProcessor RemoveSvg()
    {
        RemoveSvgInline();
        RemoveSvgFloating();
        return this;
    }

    private void ReplaceSvg(Image wordImage, string svgText)
    {
        if (string.IsNullOrEmpty(svgText)) return;
        using var svg = new SKSvg();
        svg.FromSvg(svgText);

        var rect = svg.Picture.CullRect;
        var imageWidth = rect.Width;
        var imageHeight = rect.Height;
        using var mStream = new MemoryStream();
        var maxHeight = wordImage.Height;
        var maxWidth = wordImage.Width;
        var width = (imageWidth * maxHeight) / imageHeight;
        var height = maxHeight;
        if (width > maxWidth)
        {
            width = maxWidth;
            height = (imageHeight * maxWidth) / imageWidth;
        }

        svg.Save(mStream, SKColors.White, SKEncodedImageFormat.Jpeg, 100, 4, 4);
        wordImage.ImageSource = new ImageSource(mStream, "jpg");
        wordImage.Height = height;
        wordImage.Width = width;
    
    }

    public WordProcessor ReplaceSvgStructure(string svgText, string? description = "structure")
    {
        var image = StructureImageFloating(description);
        if (image != null)
        {
            ReplaceSvg(image, svgText);
        }

        image = StructureImageInline(description);
        if (image != null)
        {
            ReplaceSvg(image, svgText);
        }

        return this;
    }

    private Image? StructureImageFloating(string description)
    {
        var structureImageInline = _document.EnumerateChildrenOfType<FloatingImage>()
            .FirstOrDefault(x => x.Image.Description == description);
        if (structureImageInline == null) return null;
        if (structureImageInline.Parent.Parent is not TableCell structureCell)
        {
            return structureImageInline.Image;
        }
        
        var table = structureCell.Row.Table;

        foreach (var tableRow in table.Rows.Skip(1))
        {
            tableRow.Cells.Remove(tableRow.Cells.Last());
        }

        structureCell.RowSpan = table.Rows.Count();

        return structureImageInline.Image;
    }

    private Image? StructureImageInline(string description)
    {
        var structureImageInline = _document.EnumerateChildrenOfType<ImageInline>()
            .FirstOrDefault(x => x.Image.Description == description);
        if (structureImageInline == null) return null;
        if (structureImageInline.Parent.Parent is not TableCell structureCell)
        {
            return structureImageInline.Image;
        }
        
        var table = structureCell.Row.Table;

        foreach (var tableRow in table.Rows.Skip(1))
        {
            tableRow.Cells.Remove(tableRow.Cells.Last());
        }

        structureCell.RowSpan = table.Rows.Count();

        return structureImageInline.Image;
    }

    public byte[] ExportToPdf()
    {
        var provider = new Telerik.Windows.Documents.Flow.FormatProviders.Pdf.PdfFormatProvider();
        using var outputStream = new MemoryStream();
        provider.Export(_document, outputStream, TimeSpan.FromSeconds(5));
        var bytes = outputStream.ToArray();
        return bytes;
    }


    public RadFixedDocument ExportRadFixedDocument()
    {
        var provider = new PdfFormatProvider();
        var fixedDocument = provider.ExportToFixedDocument(_document);
        return fixedDocument;
    }

    public WordProcessor ExportToWord(string filePath)
    {
        var provider = new DocxFormatProvider();
        using (Stream output = File.OpenWrite(filePath))
        {
            provider.Export(_document, output, TimeSpan.FromSeconds(10));
        }

        return this;
    }

    public WordProcessor AddWatermark(string text)
    {
        var settings = new TextWatermarkSettings()
        {
            Angle = -10,
            Width = 50 * text.Length,
            Height = 200,
            Opacity = 0.3,
            ForegroundColor = Color.FromRgb(146, 49, 137),
            Text = text,
        };
        var textWatermark = new Watermark(settings);
        var header = _document.Sections.First().Headers.Default;
        header.Watermarks.Add(textWatermark);
        return this;
    }

    private static void ReplaceRunsText(string text, IEnumerable<Run> runs)
    {
        if (runs.Any() is false)
        {
            return;
        }

        var firstParagraph = runs.FirstOrDefault().Paragraph;
        foreach (var run in runs.ToList())
        {
            var parapgraph = run.Paragraph;
            parapgraph.Inlines.Remove(run);
        }

        firstParagraph.Inlines.AddRun(text);
    }
}