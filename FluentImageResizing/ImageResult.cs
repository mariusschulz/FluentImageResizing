using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FluentImageResizing
{
    public class ImageResult
    {
        public byte[] Bytes { get; private set; }
        public ImageFormat Format { get; private set; }

        public CropResult Crop { get; private set; }

        public ImageResult(byte[] bytes, ImageFormat format)
        {
            Bytes = bytes;
            Format = format;

            Crop = new CropResult(this);
        }

        public Image CreateImage()
        {
            using (var memoryStream = new MemoryStream(Bytes))
            {
                return Image.FromStream(memoryStream);
            }
        }

        public ImageResult ResizeTo<TFillStrategy>(int viewportWidth, int viewportHeight) where TFillStrategy : FillStrategy, new()
        {
            var strategy = new TFillStrategy();
            var resizedImage = strategy.Resize(CreateImage(), viewportWidth, viewportHeight);
            
            SetImage(resizedImage);

            return this;
        }

        public void SetImage(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, Format);
                Bytes = memoryStream.ToArray();
            }
        }

        public void SetImage(Image image, ImageFormat format)
        {
            Format = format;
            SetImage(image);
        }
    }
}
