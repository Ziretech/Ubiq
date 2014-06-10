using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubiq2.GameLogic;

namespace Ubiq2Tests.GameLogic
{
    [TestClass]
    public class TeleporterTest
    {
        [TestMethod]
        public void TestCreate()
        {
            var teleporter = new Teleporter();
            Assert.AreEqual(new MapPosition(0, 0), teleporter.Position);
            Assert.AreEqual(new MapPosition(0, 0), teleporter.Target);
        }

        [TestMethod]
        public void TestTarget()
        {
            var teleporter = new Teleporter {Target = new MapPosition(4, 7)};
            Assert.AreEqual(new MapPosition(4, 7), teleporter.Target);
        }

        [TestMethod]
        public void TestTeleportation()
        {
            var teleporter = new Teleporter {Target = new MapPosition(6, 7)};
            var mapObject = new MapObject {Position = new MapPosition(2, 2)};
            teleporter.HandleWalkInteraction(mapObject);
            Assert.AreEqual(new MapPosition(6, 7), mapObject.Position);
        }

        [TestMethod]
        public void TestTwoTeleportations()
        {
            var teleporter = new Teleporter { Target = new MapPosition(6, 7)};
            var mapObject = new MapObject { Position = new MapPosition(2, 2)};
            teleporter.HandleWalkInteraction(mapObject);
            mapObject.Position = new MapPosition(3, 7);

            teleporter.HandleWalkInteraction(mapObject);

            Assert.AreEqual(new MapPosition(6, 7), mapObject.Position);
        }
    }
}
