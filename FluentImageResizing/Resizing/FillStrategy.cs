using System.Drawing;
using System.Drawing.Drawing2D;

namespace FluentImageResizing
{
    public abstract class FillStrategy
    {
        public abstract Image Resize(Image image, int width, int height);

        protected static Image ResizeImageToViewport(Image image, int targetWidth, int targetHeight)
        {
            var resizedImage = new Bitmap(targetWidth, targetHeight);
            resizedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(resizedImage))
            {
                SetHighestQualitySettings(graphics);
                graphics.DrawImage(image, 0, 0, targetWidth, targetHeight);
            }

            ImagePropertyItems.Copy(image, resizedImage);

            return resizedImage;
        }

        protected static void SetHighestQualitySettings(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
        }
    }
}
