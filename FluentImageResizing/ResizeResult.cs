using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FluentImageResizing
{
    public class ResizeResult
    {
        private readonly ImageResult _imageResult;

        public ResizeResult(ImageResult imageResult)
        {
            _imageResult = imageResult;
        }

        public ImageResult ToFill(int viewportWidth, int viewportHeight)
        {
            using (var original = _imageResult.CreateImage())
            {
                var image = original;
                double aspectRatio = (double)image.Width / image.Height;

                int fillWidth;
                int fillHeight;

                if (image.Width > image.Height)
                {
                    fillHeight = viewportHeight;
                    fillWidth = (int)Math.Round(viewportHeight * aspectRatio);
                }
                else
                {
                    fillWidth = viewportHeight;
                    fillHeight = (int)Math.Round(viewportHeight / aspectRatio);
                }

                image = ResizeImageToViewport(image, fillWidth, fillHeight);

                var viewportImage = new ImageCropper(image, viewportWidth, viewportHeight, CropAnchor.Center).Crop();
                _imageResult.SetImage(viewportImage);
            }

            return _imageResult;
        }

        private static Image ResizeImageToViewport(Image image, int targetWidth, int targetHeight)
        {
            var resizedImage = new Bitmap(targetWidth, targetHeight);
            resizedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(resizedImage))
            {
                SetHighestQualitySettings(graphics);
                graphics.DrawImage(image, 0, 0, targetWidth, targetHeight);
            }

            return resizedImage;
        }

        private static void SetHighestQualitySettings(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
        }
    }
}