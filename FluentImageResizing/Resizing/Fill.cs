using System;
using System.Drawing;

namespace FluentImageResizing
{
    public class Fill : FillStrategy
    {
        public override Image Resize(Image imageToResize, int viewportWidth, int viewportHeight)
        {
            using (var original = imageToResize)
            {
                var image = original;

                double aspectRatio = (double)image.Width / image.Height;
                double widthRatio = (double)viewportWidth / image.Width;
                double heightRatio = (double)viewportHeight / image.Height;

                int fillWidth;
                int fillHeight;

                if (widthRatio > heightRatio)
                {
                    fillWidth = viewportWidth;
                    fillHeight = (int)Math.Round(viewportWidth / aspectRatio);
                }
                else
                {
                    fillWidth = (int)Math.Round(viewportHeight * aspectRatio);
                    fillHeight = viewportHeight;
                }

                image = ResizeImageToViewport(image, fillWidth, fillHeight);

                return new ImageCropper(image, viewportWidth, viewportHeight, new Center()).Crop();
            }
        }
    }
}