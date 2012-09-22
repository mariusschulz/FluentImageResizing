using System;
using System.Drawing;

namespace FluentImageResizing
{
    public class Fit : FillStrategy
    {
        public override Image Resize(Image imageToResize, int viewportWidth, int viewportHeight)
        {
            using (var original = imageToResize)
            {
                var image = original;

                double aspectRatio = (double)image.Width / image.Height;
                double widthRatio = (double)viewportWidth / image.Width;
                double heightRatio = (double)viewportHeight / image.Height;

                int smallerWidth = Math.Min(image.Width, viewportWidth);
                int smallerHeight = Math.Min(image.Height, viewportHeight);

                int targetWidth;
                int targetHeight;

                if (widthRatio > heightRatio)
                {
                    targetWidth = (int)Math.Round(smallerHeight * aspectRatio);
                    targetHeight = smallerHeight;
                }
                else
                {
                    targetWidth = smallerWidth;
                    targetHeight = (int)Math.Round(smallerWidth / aspectRatio);
                }

                return ResizeImageToViewport(image, targetWidth, targetHeight);
            }
        }
    }
}