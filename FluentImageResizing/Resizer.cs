using System.Drawing.Imaging;

namespace FluentImageResizing
{
    public static class Resizer
    {
        public static ImageResult CreateImageFrom(byte[] bytes, ImageFormat format)
        {
            return new ImageResult(bytes, format);
        }
    }
}
