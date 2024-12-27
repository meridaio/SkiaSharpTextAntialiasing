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
    {
        paint.TextSize = 16;
        paint.IsAntialias = antialias;
        paint.Color = SKColors.Black;
        paint.IsStroke = false;
        paint.Typeface = SKTypeface.FromFamilyName("Trebuchet MS");

        canvas.DrawText("ANTIALIASING", 2, 16, paint);
        canvas.Flush();
    }
    canvas.Save();

    var fileName = antialias ? "working-antialias.png" : "working-no-antialias.png";

    using var image = surface.Snapshot();
    using var data = image.Encode(SKEncodedImageFormat.Png, 80);
    using var stream = File.OpenWrite(fileName);
    data.SaveTo(stream);
}


