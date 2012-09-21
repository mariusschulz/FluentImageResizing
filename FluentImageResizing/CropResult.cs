
namespace FluentImageResizing
{
    public class CropResult
    {
        public CropPositionResult FromCenter { get; set; }
        public ImageResult ImageResult { get; private set; }

        public CropResult(ImageResult imageResult)
        {
            ImageResult = imageResult;

            FromCenter = new CropPositionResult(this, CropAnchor.Center);
        }
    }
}
