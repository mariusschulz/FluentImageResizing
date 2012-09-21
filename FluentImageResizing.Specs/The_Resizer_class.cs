using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;
using Should.Fluent;

namespace FluentImageResizing.Specs
{
    [TestFixture]
    [Category("FluentImageResizing")]
    public class The_Resizer_class
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
        public void Stores_the_image_byte_data()
        {
            ImageResult resizedImageResult = Resizer.CreateImageFrom(_imageBytes, ImageFormat.Png);

            resizedImageResult.Bytes.Should().Equal(_imageBytes);
        }

        [Test]
        public void Crops_the_image_to_the_specified_size()
        {
            Image image = Resizer
                .CreateImageFrom(_imageBytes, ImageFormat.Png)
                .Crop.FromCenter.ToAtMost(75, 60)
                .CreateImage();

            image.Width.Should().Equal(75);
            image.Height.Should().Equal(60);
        }

        [Test]
        public void Uses_the_image_size_if_the_image_to_crop_is_smaller_than_the_crop_size()
        {
            Image image = Resizer
                .CreateImageFrom(_iconBytes, ImageFormat.Png)
                .Crop.FromCenter.ToAtMost(75, 60)
                .CreateImage();

            image.Width.Should().Equal(16);
            image.Height.Should().Equal(16);
        }
    }
}
