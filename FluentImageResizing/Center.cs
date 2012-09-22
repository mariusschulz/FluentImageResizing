using System.Drawing;

namespace FluentImageResizing
{
    public class Center : CropAnchor
    {
        public override Rectangle GetSourceClip(int imageWidth, int imageHeight, int cropWidth, int cropHeight)
        {
            int left = (imageWidth - cropWidth) / 2;
            int top = (imageHeight - cropHeight) / 2;

            return new Rectangle(left, top, cropWidth, cropHeight);
        }
    }
}