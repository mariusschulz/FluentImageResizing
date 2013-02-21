using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FluentImageResizing
{
    public static class Resizer
    {
        public static ImageResult CreateImageFrom(byte[] bytes, ImageFormat format)
        {
            byte[] nonIndexedImageBytes;

            using (var memoryStream = new MemoryStream(bytes))
            using (var image = Image.FromStream(memoryStream))
            {
                using (var temporaryBitmap = new Bitmap(image.Width, image.Height))
                {
                    using (var graphics = Graphics.FromImage(temporaryBitmap))
                    {
                        graphics.DrawImage(image, new Rectangle(0, 0, temporaryBitmap.Width, temporaryBitmap.Height),
                            0, 0, temporaryBitmap.Width, temporaryBitmap.Height, GraphicsUnit.Pixel);
                    }

                    using (var saveStream = new MemoryStream())
                    {
                        temporaryBitmap.Save(saveStream, format);
                        nonIndexedImageBytes = saveStream.ToArray();
                    }
                }
            }

            return new ImageResult(nonIndexedImageBytes, format);
        }
    }
}
