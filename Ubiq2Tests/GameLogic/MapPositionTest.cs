using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubiq2.GameLogic;

namespace Ubiq2Tests.GameLogic
{
    [TestClass]
    public class MapPositionTest
    {
        [TestMethod]
        public void TestX()
        {
            var position = new MapPosition(5, 0);
            Assert.AreEqual(5, position.X);
        }

        [TestMethod]
        public void TestY()
        {
            var position = new MapPosition(0, 8);
            Assert.AreEqual(8, position.Y);
        }

        [TestMethod]
        public void TestCreatePositionToTheLeftAndUp()
        {
            var position = new MapPosition(4, 5);
            var newPosition = new MapPosition(position, new PositionChange {X = -1, Y = 1});
            Assert.AreEqual(3, newPosition.X);
            Assert.AreEqual(6, newPosition.Y);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestCreatePositionFromNull()
        {
            var position = new MapPosition(null);
        }

        [TestMethod]
        public void TestEquality()
        {
            var position = new MapPosition(5, 5);
            var samePosition = new MapPosition(5, 5);
            var otherXPosition = new MapPosition(3, 5);
            var otherYPosition = new MapPosition(5, 7);

            // general
            Assert.AreEqual(position, position);
            Assert.AreNotEqual(position, null);
            Assert.AreNotEqual(position, new object());
            // MapPosition
            Assert.AreEqual(position, samePosition);
            Assert.AreEqual(position, (object)samePosition);
            Assert.AreNotEqual(position, otherXPosition);
            Assert.AreNotEqual(position, otherYPosition);
            
        }

        [TestMethod]
        public void TestToString()
        {
            var position = new MapPosition(8, 3);
            Assert.AreEqual("8, 3", position.ToString());
        }
    }
}
