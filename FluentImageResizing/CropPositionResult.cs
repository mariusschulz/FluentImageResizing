using System;
using System.Drawing;

namespace FluentImageResizing
{
    public class CropPositionResult
    {
        private readonly CropResult _cropResult;
        private readonly CropAnchor _anchor;

        public CropPositionResult(CropResult cropResult, CropAnchor anchor)
        {
            _cropResult = cropResult;
            _anchor = anchor;
        }

        public ImageResult ToAtMost(int maxWidth, int maxHeight)
        {
            using (var image = _cropResult.ImageResult.CreateImage())
            {
                int cropWidth = Math.Min(image.Width, maxWidth);
                int cropHeight = Math.Min(image.Height, maxHeight);

                using (var croppedImage = new Bitmap(cropWidth, cropHeight))
                using (var graphics = Graphics.FromImage(croppedImage))
                {
                    var destinationClip = new Rectangle(0, 0, cropWidth, cropHeight);
                    var sourceClip = GetSourceClip(image, cropWidth, cropHeight);

                    graphics.DrawImage(image, destinationClip, sourceClip, GraphicsUnit.Pixel);

                    _cropResult.ImageResult.SetImage(croppedImage);
                }
            }

            return _cropResult.ImageResult;
        }

        private Rectangle GetSourceClip(Image image, int cropWidth, int cropHeight)
        {
            Rectangle sourceClip;

            switch (_anchor)
            {
                default:
                    int left = (image.Width - cropWidth) / 2;
                    int top = (image.Height - cropHeight) / 2;
                    sourceClip = new Rectangle(left, top, cropWidth, cropHeight);
                    break;
            }

            return sourceClip;
        }
    }
}