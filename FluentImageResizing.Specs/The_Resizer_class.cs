using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;
using Should.Fluent;

namespace FluentImageResizing.Specs
{
    #region When an image is read by the Resizer class

    [TestFixture]
    public class When_an_image_is_read_by_the_Resizer_class
    {
        private byte[] _imageBytes;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _imageBytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "../../image.png"));
        }

        [Test]
        public void The_image_data_is_stored_correctly()
        {
            Resizer.CreateImageFrom(_imageBytes, ImageFormat.Png).Bytes.Should().Equal(_imageBytes);
        }
    }

    #endregion

    #region When an image is cropped to the specified maximum dimensions

    [TestFixture]
    public class When_an_image_is_cropped_to_the_specified_maximum_dimensions
    {
        private byte[] _imageBytes;
        private byte[] _iconBytes;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _imageBytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "../../image.png"));
            _iconBytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "../../icon.png"));
        }

        [Test]
        public void The_dimensions_of_the_resulting_image_equal_the_specified_dimensions_if_the_image_dimensions_are_bigger_than_the_specified_ones()
        {
            Image image = Resizer
                 .CreateImageFrom(_imageBytes, ImageFormat.Png)
                 .Crop.FromCenter.ToAtMost(75, 60)
                 .CreateImage();

            image.Width.Should().Equal(75);
            image.Height.Should().Equal(60);
        }

        [Test]
        public void The_dimensions_of_the_resulting_image_equal_the_image_dimensions_if_the_image_dimensions_are_smaller_than_the_specified_ones()
        {
            Image image = Resizer
                .CreateImageFrom(_iconBytes, ImageFormat.Png)
                .Crop.FromCenter.ToAtMost(75, 60)
                .CreateImage();

            image.Width.Should().Equal(16);
            image.Height.Should().Equal(16);
        }
    }

    #endregion
}
