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
                var temporaryBitmap = new Bitmap(image.Width, image.Height);

                using (Graphics grPhoto = Graphics.FromImage(temporaryBitmap))
                {
                    grPhoto.DrawImage(temporaryBitmap, new Rectangle(0, 0, temporaryBitmap.Width, temporaryBitmap.Height),
                        0, 0, temporaryBitmap.Width, temporaryBitmap.Height, GraphicsUnit.Pixel);
                }
               
                using (var saveStream = new MemoryStream())
                {
                    temporaryBitmap.Save(saveStream, format);
                    nonIndexedImageBytes = saveStream.ToArray();
                } 
            }

            return new ImageResult(nonIndexedImageBytes, format);
        }
    }
}
