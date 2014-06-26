using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ubiq2.GameLogic;

namespace Ubiq2Tests.GameLogic
{
    [TestClass]
    public class PositionChangeTest
    {
        [TestMethod]
        public void TestCreatedHaveNoChange()
        {
            var change = new PositionChange();
            Assert.AreEqual(0, change.X);
            Assert.AreEqual(0, change.Y);
        }

        [TestMethod]
        public void TestChangeSetTo2_5()
        {
            var change = new PositionChange {X = 2, Y = 5};
            Assert.AreEqual(2, change.X);
            Assert.AreEqual(5, change.Y);
        }

        [TestMethod]
        public void TestEquality()
        {
            var change = new PositionChange { X = 5, Y = 5 };
            var sameChange = new PositionChange { X = 5, Y = 5 };
            var otherXChange = new PositionChange { X = 3, Y = 5 };
            var otherYChange = new PositionChange { X = 5, Y = 7 };

            // general
            Assert.AreEqual(change, change);
            Assert.AreNotEqual(change, null);
            Assert.AreNotEqual(change, new object());
            // MapPosition
            Assert.AreEqual(change, sameChange);
            Assert.AreEqual(change, (object)sameChange);
            Assert.AreNotEqual(change, otherXChange);
            Assert.AreNotEqual(change, otherYChange);

        }

        [TestMethod]
        public void TestHashCode()
        {
            var baseChange = new PositionChange {X = 3, Y = 7};
            var sameChange = new PositionChange {X = 3, Y = 7};
            var differInXChange = new PositionChange {X = 2, Y = 7};
            var differInYChange = new PositionChange {X = 3, Y = 8};

            Assert.AreEqual(baseChange.GetHashCode(), sameChange.GetHashCode());
            Assert.AreNotEqual(baseChange.GetHashCode(), differInXChange.GetHashCode());
            Assert.AreNotEqual(baseChange.GetHashCode(), differInYChange.GetHashCode());
        }

        [TestMethod]
        public void TestCreateLeft()
        {
            Assert.AreEqual(new PositionChange {X = -1}, PositionChange.CreateLeft());
        }

        [TestMethod]
        public void TestCreateRight()
        {
            Assert.AreEqual(new PositionChange { X = 1 }, PositionChange.CreateRight());
        }

        [TestMethod]
        public void TestCreateDown()
        {
            Assert.AreEqual(new PositionChange { Y = -1 }, PositionChange.CreateDown());
        }

        [TestMethod]
        public void TestCreateUp()
        {
            Assert.AreEqual(new PositionChange { Y = 1 }, PositionChange.CreateUp());
        }
    }
}
