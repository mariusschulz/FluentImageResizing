using System.Drawing;

namespace FluentImageResizing
{
    public abstract class CropAnchor
    {
        public abstract Rectangle GetSourceClip(int imageWidth, int imageHeight, int cropWidth, int cropHeight);
    }
}