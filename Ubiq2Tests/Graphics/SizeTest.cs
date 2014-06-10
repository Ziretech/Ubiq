using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubiq2.Graphics;

namespace Ubiq2Tests.Graphics
{
    [TestClass]
    public class SizeTest
    {
        [TestMethod]
        public void TestWidth()
        {
            var size = new Size(5, 1);
            Assert.AreEqual(5, size.Width);
        }

        [TestMethod]
        public void TestHeight()
        {
            var size = new Size(1, 8);
            Assert.AreEqual(8, size.Height);
        }

        [TestMethod]
        public void TestCreateSizeFromSize()
        {
            var size = new Size(7, 4);
            Assert.AreEqual(new Size(7, 4), new Size(size));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestCreateSizeFromNull()
        {
            new Size(null);
        }

        [TestMethod]
        public void TestEquality()
        {
            var size = new Size(5, 5);
            var sameSize = new Size(5, 5);
            var otherWidth = new Size(3, 5);
            var otherHeight = new Size(5, 7);

            // general
            Assert.AreEqual(size, size);
            Assert.AreNotEqual(size, null);
            Assert.AreNotEqual(size, new object());
            // MapPosition
            Assert.AreEqual(size, sameSize);
            Assert.AreEqual(size, (object)sameSize);
            Assert.AreNotEqual(size, otherWidth);
            Assert.AreNotEqual(size, otherHeight);

        }

        [TestMethod]
        public void TestToString()
        {
            var size = new Size(4, 6);
            Assert.AreEqual("4, 6", size.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestZeroWidthIllegal()
        {
            var size = new Size(0, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegative2WidthIllegal()
        {
            var size = new Size(-2, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestZeroHeightIllegal()
        {
            var size = new Size(3, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegative5HeightIllegal()
        {
            var size = new Size(1, -5);
        }
    }
}
