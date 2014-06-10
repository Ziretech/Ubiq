using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubiq2.GameLogic;

namespace Ubiq2Tests.GameLogic
{
    [TestClass]
    public class MapObjectTest
    {
        [TestMethod]
        public void TestNonInitHavePositionAtOrigo()
        {
            var movableObject = new MapObject();
            
            Assert.AreEqual(new MapPosition(0, 0), movableObject.Position);
        }

        [TestMethod]
        public void TestCreatedAreBlocking()
        {
            var movableObject = new MapObject();

            Assert.IsTrue(movableObject.Blocking);
        }

        [TestMethod]
        public void TestPosition()
        {
            var movableObject = new MapObject {Position = new MapPosition(2, 4)};
            
            Assert.AreEqual(new MapPosition(2, 4), movableObject.Position);
        }

        [TestMethod]
        public void TestMoveUp()
        {
            var movableObject = new MapObject {Position = new MapPosition(5, 7)};

            movableObject.Position = new MapPosition(movableObject.Position, new PositionChange {Y = 1});

            Assert.AreEqual(new MapPosition(5, 8), movableObject.Position);
        }

        [TestMethod]
        public void TestMoveDown()
        {
            var movableObject = new MapObject {Position = new MapPosition(8, 1)};

            movableObject.Position = new MapPosition(movableObject.Position, new PositionChange{Y = -1});

            Assert.AreEqual(new MapPosition(8, 0), movableObject.Position);
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            var movableObject = new MapObject {Position = new MapPosition(4, 4)};

            movableObject.Position = new MapPosition(movableObject.Position, new PositionChange {X = -1});

            Assert.AreEqual(new MapPosition(3, 4), movableObject.Position);
        }

        [TestMethod]
        public void TestMoveRight()
        {
            var movableObject = new MapObject {Position = new MapPosition(6, 2)};

            movableObject.Position = new MapPosition(movableObject.Position, new PositionChange{X = 1});

            Assert.AreEqual(new MapPosition(7, 2), movableObject.Position);
        }

        [TestMethod]
        public void TestNonBlocking()
        {
            var mapObject = new MapObject {Blocking = false};

            Assert.IsFalse(mapObject.Blocking);
        }
    }
}
