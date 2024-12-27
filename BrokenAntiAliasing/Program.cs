using SkiaSharp;

Draw(true);
Draw(false);

static void Draw(bool antialias)
{
    var skInfo = new SKImageInfo(100, 20);
    using var surface = SKSurface.Create(skInfo);
    var canvas = surface.Canvas;
    canvas.Clear(SKColors.White);

    // draw left-aligned text, solid
    using (var paint = new SKPaint())
    using (var font = new SKFont(SKTypeface.FromFamilyName("Trebuchet MS"), 16))
    {
        paint.IsAntialias = antialias;
        paint.Color = SKColors.Black;
        paint.IsStroke = false;

        canvas.DrawText("ANTIALIASING", 2, 16, SKTextAlign.Left, font, paint);
        canvas.Flush();
    }
    canvas.Save();

    var fileName = antialias ? "broken-antialias.png" : "broken-no-antialias.png";

    using var image = surface.Snapshot();
    using var data = image.Encode(SKEncodedImageFormat.Png, 80);
    using var stream = File.OpenWrite(fileName);
    data.SaveTo(stream);
}
