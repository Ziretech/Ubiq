using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubiq2.Graphics;

namespace Ubiq2Tests.Graphics
{
    [TestClass]
    public class TileTest
    {
        [TestMethod]
        public void TestCreate()
        {
            var tile = new Tile();
            Assert.AreEqual(new Size(1, 1), tile.Size);
        }

        [TestMethod]
        public void TestSize()
        {
            var tile = new Tile {Size = new Size(13, 6)};
            Assert.AreEqual(new Size(13, 6), tile.Size);
        }
    }
}
