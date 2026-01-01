using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ZXing.Common;

namespace QRCoder_Avalonia.Views.Controls
{
    public class BarcodeCanvas : Control
    {
        public static readonly StyledProperty<BitMatrix?> BarcodeMatrixProperty =
            AvaloniaProperty.Register<BarcodeCanvas, BitMatrix?>(nameof(BarcodeMatrix));

        public BitMatrix? BarcodeMatrix
        {
            get => GetValue(BarcodeMatrixProperty);
            set => SetValue(BarcodeMatrixProperty, value);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == BarcodeMatrixProperty)
            {
                InvalidateVisual();
            }
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            var bounds = new Rect(Bounds.Size);
            // background
            context.FillRectangle(Brushes.White, bounds);

            var matrix = BarcodeMatrix;
            if (matrix == null)
                return;

            int mw = matrix.Width;
            int mh = matrix.Height;
            if (mw == 0 || mh == 0)
                return;

            double scaleX = Bounds.Width / mw;
            double scaleY = Bounds.Height / mh;
            double scale = Math.Min(scaleX, scaleY);

            // center the barcode
            double offsetX = (Bounds.Width - mw * scale) / 2.0;
            double offsetY = (Bounds.Height - mh * scale) / 2.0;

            var black = Brushes.Black;

            for (int y = 0; y < mh; y++)
            {
                for (int x = 0; x < mw; x++)
                {
                    if (matrix[x, y])
                    {
                        var rect = new Rect(offsetX + x * scale, offsetY + y * scale, scale, scale);
                        context.FillRectangle(black, rect);
                    }
                }
            }
        }
    }
}