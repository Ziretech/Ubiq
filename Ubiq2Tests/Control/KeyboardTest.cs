using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using OpenTK.Input;
using Keyboard = Ubiq2.Control.Keyboard;

namespace Ubiq2Tests.Control
{
    [TestClass]
    public class KeyboardTest
    {
        [TestMethod]
        public void TestInternalKeyboard()
        {
            var window = new GameWindow();
            var keyboard = new Keyboard {InternalKeyboard = window.Keyboard};
            Assert.AreEqual(window.Keyboard, keyboard.InternalKeyboard);
        }

        [TestMethod]
        public void TestReadKeyStillDown()
        {
            bool readKey = false;
            var window = new GameWindow();
            var keyboard = new Keyboard {InternalKeyboard = window.Keyboard};
            window.RenderFrame += (sender, args) =>
            {
                if(keyboard.Pressed(Key.Number1))
                {
                    readKey = true;
                    window.Close();
                }
            };
            window.Run();
            Assert.IsTrue(readKey);
        }

        [TestMethod]
        public void TestReleasedReadKey()
        {
            bool readKey = false;
            int numPressedKeys = 0;
            var window = new GameWindow();
            var keyboard = new Keyboard {InternalKeyboard = window.Keyboard};
            window.Keyboard.KeyDown += (sender, args) => numPressedKeys ++;
            window.Keyboard.KeyUp += (sender, args) => numPressedKeys --;
            window.RenderFrame += (sender, args) =>
            {
                if (numPressedKeys < 1 && keyboard.Pressed(Key.Number2))
                {
                    readKey = true;
                    window.Close();
                }
            };
            window.Run();
            Assert.IsTrue(readKey);
        }
    }
}
