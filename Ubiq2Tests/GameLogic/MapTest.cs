using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubiq2.GameLogic;

namespace Ubiq2Tests.GameLogic
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void TestNonInitHaveNoObjects()
        {
            var map = new Map();
            Assert.AreEqual(0, map.GetNumDrawables());
        }

        [TestMethod]
        public void TestAddOneMovableObject()
        {
            var map = new Map();
            var movableObject = new MapObject();
            map.AddObject(movableObject);

            Assert.AreEqual(1, map.GetNumDrawables());
            Assert.AreEqual(movableObject, map.GetDrawable(0));
        }

        [TestMethod]
        public void TestAddTwoMovableObjects()
        {
            var map = new Map();
            var movableObject1 = new MapObject();
            map.AddObject(movableObject1);
            var movableObject2 = new MapObject();
            map.AddObject(movableObject2);

            Assert.AreEqual(2, map.GetNumDrawables());
            Assert.AreEqual(movableObject1, map.GetDrawable(0));
            Assert.AreEqual(movableObject2, map.GetDrawable(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddNullObjectThrowsException()
        {
            var map = new Map();
            map.AddObject(null);
        }

        [TestMethod]
        public void TestMoveUpNoCollision()
        {
            TestMoveDirectionWithTwoObjects(
                new MapPosition(4, 4), 
                new MapPosition(4, 6), 
                new PositionChange { Y = 1 }, 
                new MapPosition(4, 5));
        }

        [TestMethod]
        public void TestMoveUpCollisionWithObject()
        {
            TestMoveDirectionWithTwoObjects(
                new MapPosition(4, 4), 
                new MapPosition(4, 5), 
                new PositionChange { Y = 1 }, 
                new MapPosition(4, 4));
        }

        [TestMethod]
        public void TestMoveDownNoCollision()
        {
            TestMoveDirectionWithTwoObjects(
                new MapPosition(4, 4),
                new MapPosition(4, 2),
                new PositionChange { Y = -1},
                new MapPosition(4, 3));
        }

        [TestMethod]
        public void TestMoveDownCollisionWithObject()
        {
            TestMoveDirectionWithTwoObjects(
                new MapPosition(4, 4),
                new MapPosition(4, 3),
                new PositionChange { Y = -1 },
                new MapPosition(4, 4));
        }

        [TestMethod]
        public void TestMoveRightNoCollision()
        {
            TestMoveDirectionWithTwoObjects(
                new MapPosition(4, 4),
                new MapPosition(6, 4),
                new PositionChange {X = 1},
                new MapPosition(5, 4));
        }

        [TestMethod]
        public void TestMoveRightCollision()
        {
            TestMoveDirectionWithTwoObjects(
                new MapPosition(4, 4),
                new MapPosition(5, 4),
                new PositionChange { X = 1 },
                new MapPosition(4, 4));
        }

        [TestMethod]
        public void TestMoveLeftNoCollision()
        {
            TestMoveDirectionWithTwoObjects(
                new MapPosition(4, 4),
                new MapPosition(2, 4),
                new PositionChange { X = -1 },
                new MapPosition(3, 4));
        }

        [TestMethod]
        public void TestMoveLeftCollision()
        {
            TestMoveDirectionWithTwoObjects(
                new MapPosition(4, 4),
                new MapPosition(3, 4),
                new PositionChange { X = -1 },
                new MapPosition(4, 4));
        }

        private void TestMoveDirectionWithTwoObjects(
            MapPosition movablePosition,
            MapPosition staticPosition,
            PositionChange direction,
            MapPosition positionAfterMove)
        {
            var map = new Map();
            var staticObject = new MapObject { Position = staticPosition };
            var moveableObject = new MapObject { Position = movablePosition };
            map.AddObject(staticObject);
            map.AddObject(moveableObject);

            map.Move(moveableObject, direction);
            Assert.AreEqual(positionAfterMove, moveableObject.Position);
        }

        [TestMethod]
        public void TestBlockingObjectCanShareCoordinateWithNonBlocking()
        {
            var map = new Map();
            var blockingObject = new MapObject {Position = new MapPosition(4, 4)};
            var nonBlockingObject = new MapObject {Position = new MapPosition(4, 3), Blocking = false};
            map.AddObject(blockingObject);
            map.AddObject(nonBlockingObject);

            map.Move(blockingObject, new PositionChange {Y = -1});

            Assert.AreEqual(new MapPosition(4, 3), blockingObject.Position);
        }

        [TestMethod]
        public void TestNonBlockingObjectCanShareCoordinateWithBlocking()
        {
            var map = new Map();
            var blockingObject = new MapObject { Position = new MapPosition(4, 4), Blocking = false };
            var nonBlockingObject = new MapObject { Position = new MapPosition(4, 3), Blocking = false };
            map.AddObject(blockingObject);
            map.AddObject(nonBlockingObject);

            map.Move(blockingObject, new PositionChange { Y = -1 });

            Assert.AreEqual(new MapPosition(4, 3), blockingObject.Position);
        }

        [TestMethod]
        public void TestAdd7500ObjectsAndMove100Times()
        {
            var map = new Map();
            for (int i = 0; i < 10; i++)
            {
                for (int x = 0; x < 30; x++)
                {
                    for (int y = 0; y < 25; y++)
                    {
                        var mapObject = new MapObject {Position = new MapPosition(x, y), Blocking = true};
                        map.AddObject(mapObject);
                    }
                }
            }

            var character = new MapObject {Position = new MapPosition(50, 50), Blocking = true};
            map.AddObject(character);
            for (int i = 0; i < 100; i++)
            {
                map.Move(character, new PositionChange {X = 1});
            }
        }

        [TestMethod]
        public void TestInteractWithObject()
        {
            var mapItem = new MapObject {Position = new MapPosition(5, 5), Blocking = true};
            var character = new MapObject {Position = new MapPosition(4, 5), Blocking = true};
            var map = new Map();
            map.AddObject(mapItem);
            map.AddObject(character);
            map.Move(character, new PositionChange {X = 1});
        }
    }
}
