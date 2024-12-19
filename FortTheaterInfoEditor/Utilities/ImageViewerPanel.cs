using System;
using System.Drawing;
using System.Windows.Forms;
using SkiaSharp;

public class ImageViewerPanel : Panel
{
    private SKBitmap _image;
    private SKColor _backgroundColor = new SKColor(240, 240, 240);
    private int _padding = 10;
    private bool _showBorder = true;
    private Color _borderColor = Color.Gray;

    public ImageViewerPanel()
    {
        DoubleBuffered = true;
    }

    public void SetImage(SKBitmap image)
    {
        _image = image;
        Invalidate();
    }

    public void SetBackgroundColor(SKColor backgroundColor)
    {
        _backgroundColor = backgroundColor;
        Invalidate();
    }

    public void SetPadding(int padding)
    {
        _padding = padding;
        Invalidate();
    }

    public void ShowBorder(bool show, Color? borderColor = null)
    {
        _showBorder = show;
        if (borderColor.HasValue)
        {
            _borderColor = borderColor.Value;
        }
        Invalidate();
    }

    public void ClearImage()
    {
        _image = null;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var g = e.Graphics;
        var panelWidth = Width;
        var panelHeight = Height;

        using (var surface = SKSurface.Create(new SKImageInfo(panelWidth, panelHeight)))
        {
            var canvas = surface.Canvas;

            canvas.Clear(_backgroundColor);

            if (_image != null)
            {
                float scaleWidth = (panelWidth - 2 * _padding) / (float)_image.Width;
                float scaleHeight = (panelHeight - 2 * _padding) / (float)_image.Height;
                float scale = Math.Min(scaleWidth, scaleHeight);

                int newWidth = (int)(_image.Width * scale);
                int newHeight = (int)(_image.Height * scale);
                int x = (panelWidth - newWidth) / 2;
                int y = (panelHeight - newHeight) / 2;

                var destRect = new SKRect(x, y, x + newWidth, y + newHeight);
                canvas.DrawBitmap(_image, destRect);
            }

            if (_showBorder)
            {
                using (var borderPaint = new SKPaint
                {
                    Color = new SKColor(_borderColor.R, _borderColor.G, _borderColor.B),
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 2
                })
                {
                    canvas.DrawRect(0, 0, panelWidth, panelHeight, borderPaint);
                }
            }

            using (var skImage = surface.Snapshot())
            using (var data = skImage.Encode(SKEncodedImageFormat.Png, 100))
            using (var memoryStream = new System.IO.MemoryStream(data.ToArray()))
            using (var bitmap = new Bitmap(memoryStream))
            {
                g.DrawImage(bitmap, 0, 0, panelWidth, panelHeight);
            }
        }
    }
}
