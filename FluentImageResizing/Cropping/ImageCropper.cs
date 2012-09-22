using System;
using System.Drawing;

namespace FluentImageResizing
{
    internal class ImageCropper
    {
        private readonly Image _image;
        private readonly int _maxWidth;
        private readonly int _maxHeight;
        private readonly CropAnchor _anchor;
        private int _cropWidth;
        private int _cropHeight;
        private Bitmap _croppedImage;

        public ImageCropper(Image image, int maxWidth, int maxHeight, CropAnchor anchor)
        {
            _image = image;
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
            _anchor = anchor;
        }

        public Image Crop()
        {
            CalculateCropSizes();
            CreateEmptyBitmap();
            DrawCroppedImageOnBitmap();

            return _croppedImage;
        }

        private void CalculateCropSizes()
        {
            _cropWidth = Math.Min(_image.Width, _maxWidth);
            _cropHeight = Math.Min(_image.Height, _maxHeight);
        }

        private void CreateEmptyBitmap()
        {
            _croppedImage = new Bitmap(_cropWidth, _cropHeight);
        }

        private void DrawCroppedImageOnBitmap()
        {
            using (var graphics = Graphics.FromImage(_croppedImage))
            {
                var sourceClip = _anchor.GetSourceClip(_image.Width, _image.Height, _cropWidth, _cropHeight);
                var destinationClip = new Rectangle(0, 0, _cropWidth, _cropHeight);

                graphics.DrawImage(_image, destinationClip, sourceClip, GraphicsUnit.Pixel);
            }
        }
    }
}
