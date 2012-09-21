using System;
using System.IO;
using NUnit.Framework;

namespace FluentImageResizing.Specs
{
    [TestFixture]
    public abstract class TestFixtureWorkingWithImages
    {
        protected byte[] ImageBytes300x200;
        protected byte[] ImageBytes200x300;
        protected byte[] IconBytes16x16;

        [TestFixtureSetUp]
        public void SetUp()
        {
            string imagePath300x200 = Path.Combine(Environment.CurrentDirectory, "../../image300x200.png");
            ImageBytes300x200 = File.ReadAllBytes(imagePath300x200);

            string imagePath200x300 = Path.Combine(Environment.CurrentDirectory, "../../image200x300.png");
            ImageBytes200x300 = File.ReadAllBytes(imagePath200x300);

            string iconPath = Path.Combine(Environment.CurrentDirectory, "../../icon.png");
            IconBytes16x16 = File.ReadAllBytes(iconPath);
        }
    }
}
