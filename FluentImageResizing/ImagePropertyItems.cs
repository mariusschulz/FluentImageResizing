using System.Drawing;

namespace FluentImageResizing
{
    public static class ImagePropertyItems
    {
        public static void Copy(Image source, Image target)
        {
            foreach (var propertyItem in source.PropertyItems)
            {
                target.SetPropertyItem(propertyItem);
            }
        }
    }
}
