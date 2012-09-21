
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
                var croppedImage = new ImageCropper(image, maxWidth, maxHeight, _anchor).Crop();
                _cropResult.ImageResult.SetImage(croppedImage);

                return _cropResult.ImageResult;
            }
        }
    }
}