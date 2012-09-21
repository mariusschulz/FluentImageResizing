using System;
using System.IO;
using NUnit.Framework;

namespace FluentImageResizing.Specs
{
    [TestFixture]
    public abstract class TestFixtureWorkingWithImages
    {
        protected byte[] ImageBytes;
        protected byte[] IconBytes;

        [TestFixtureSetUp]
        public void SetUp()
        {
            ImageBytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "../../image.png"));
            IconBytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "../../icon.png"));
        }
    }
}
