using System.Drawing;
using System.Drawing.Imaging;
using NUnit.Framework;
using Should.Fluent;

namespace FluentImageResizing.Specs
{
    #region When an image is cropped to the specified maximum dimensions

    public class When_an_image_is_cropped_to_the_specified_maximum_dimensions : TestFixtureWorkingWithImages
    {
        [Test]
        public void The_dimensions_of_the_resulting_image_equal_the_specified_dimensions_if_the_image_dimensions_are_bigger_than_the_specified_ones()
        {
            Image image = Resizer
                 .CreateImageFrom(ImageBytes300x200, ImageFormat.Png)
                 .Crop.FromCenter.ToAtMost(75, 60)
                 .CreateImage();

            image.Width.Should().Equal(75);
            image.Height.Should().Equal(60);
        }

        [Test]
        public void The_dimensions_of_the_resulting_image_equal_the_image_dimensions_if_the_image_dimensions_are_smaller_than_the_specified_ones()
        {
            Image image = Resizer
                .CreateImageFrom(IconBytes16x16, ImageFormat.Png)
                .Crop.FromCenter.ToAtMost(75, 60)
                .CreateImage();

            image.Width.Should().Equal(16);
            image.Height.Should().Equal(16);
        }
    }

    #endregion

    #region When an image is resized to fill the specified dimensions

    public class When_an_image_is_resized_to_fill_the_specified_dimensions : TestFixtureWorkingWithImages
    {
        [TestCase(600, 800)]
        [TestCase(200, 200)]
        [TestCase(200, 800)]
        [TestCase(300, 200)]
        [TestCase(200, 300)]
        [TestCase(150, 100)]
        [TestCase(100, 150)]
        [TestCase(10, 10)]
        public void The_resulting_image_dimensions_equal_the_specified_ones(int width, int height)
        {
            var landscapeImage = Resizer.CreateImageFrom(ImageBytes300x200, ImageFormat.Png).Resize.ToFill(width, height).CreateImage();
            landscapeImage.Size.Should().Equal(new Size { Width = width, Height = height });

            var portraitImage = Resizer.CreateImageFrom(ImageBytes200x300, ImageFormat.Png).Resize.ToFill(width, height).CreateImage();
            portraitImage.Size.Should().Equal(new Size { Width = width, Height = height });
        }
    }

    #endregion
}
