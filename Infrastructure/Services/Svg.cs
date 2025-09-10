using SkiaSharp;
using Svg.Skia;
using Telerik.SvgIcons;

namespace VeriShip.Infrastructure.Services;

public static class Svg
{
    const  int SIZE = 512;
    public static string ToSvg(this ISvgIcon svgIcon, int factor = 1)
    {
        var size = SIZE * factor;
     return   $"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 {size} {size}'>{svgIcon.Content}</svg>";
    }
    
    public static byte[] SvgToJpeg( string svgText)
    {
        using (var svg = new SKSvg())
        {
            svg.FromSvg(svgText);
            var rect = svg.Picture.CullRect;

            using (var mStream = new MemoryStream())
            {
                svg.Save(mStream, SKColors.White, SKEncodedImageFormat.Jpeg, 100, 4, 4);
                return mStream.ToArray();
            }
          
        }
    }
    
    public static readonly string Caffeine = @"
<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 300 300'>
    <circle cx='150' cy='50' r='10' fill='black' />
    <circle cx='70' cy='100' r='10' fill='black' />
    <circle cx='230' cy='100' r='10' fill='black' />
    <circle cx='110' cy='200' r='10' fill='black' />
    <circle cx='190' cy='200' r='10' fill='black' />
    
    <line x1='150' y1='50' x2='70' y2='100' stroke='black' stroke-width='2' />
    <line x1='150' y1='50' x2='230' y2='100' stroke='black' stroke-width='2' />
    <line x1='70' y1='100' x2='110' y2='200' stroke='black' stroke-width='2' />
    <line x1='230' y1='100' x2='190' y2='200' stroke='black' stroke-width='2' />
    <line x1='110' y1='200' x2='190' y2='200' stroke='black' stroke-width='2' />
    
    <!-- Cyclic bonds -->
    <line x1='70' y1='100' x2='150' y2='150' stroke='black' stroke-width='2' />
    <line x1='230' y1='100' x2='150' y2='150' stroke='black' stroke-width='2' />
    
    <!-- Structural labels -->
    <text x='120' y='40' font-size='14' fill='black'>CH3</text>
    <text x='35' y='100' font-size='14' fill='black'>N</text>
    <text x='245' y='100' font-size='14' fill='black'>N</text>
    <text x='95' y='220' font-size='14' fill='black'>N</text>
    <text x='195' y='220' font-size='14' fill='black'>N</text>
    <text x='140' y='170' font-size='14' fill='black'>O</text>
</svg>";
}