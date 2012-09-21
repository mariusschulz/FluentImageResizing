using System;
using System.IO;
using NUnit.Framework;

namespace FluentImageResizing.Specs
{
    [TestFixture]
    public abstract class TestFixtureWorkingWithImages
    {
        protected byte[] ImageBytes300x200;
        protected byte[] IconBytes16x16;

        [TestFixtureSetUp]
        public void SetUp()
        {
            string imagePath = Path.Combine(Environment.CurrentDirectory, "../../image.png");
            ImageBytes300x200 = File.ReadAllBytes(imagePath);

            string iconPath = Path.Combine(Environment.CurrentDirectory, "../../icon.png");
            IconBytes16x16 = File.ReadAllBytes(iconPath);
        }
    }
}
