using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Ubiq2.GameLogic;
using Ubiq2.Graphics;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Ubiq2Tests.Graphics
{
    [TestClass]
    public class GraphicTest
    {
        [TestMethod]
        public void TestShowQuad()
        {
            var pixels = new[] {new Pixel(), new Pixel(), new Pixel(), new Pixel()};
            new Graphic
            {
                Gl = new GLWrapper(), 
                Window = new GameWindow { Width = 200, Height = 200 }, 
                TextureFileName = @"..\..\Images\textures.png",
                DetermineScreenSizeInTiles = (SizeInPixels size) => new Vector2d(2, 2),
                QuadList = new List<Quad> { new Quad{FragmentPositionY = 3.0}},
                EndRedrawAction = (Graphic graphic) =>
                {
                    pixels[0].ReadBuffer(47, 70);
                    pixels[1].ReadBuffer(46, 89);
                    pixels[2].ReadBuffer(51, 35);
                    pixels[3].ReadBuffer(51, 33);
                    graphic.Window.Close();
                }
            }.Run();

            var faceColor = new Pixel(0.9058824f, 0.7882353f, 0.6196079f);
            var hairColor = new Pixel(0.627451f, 0.4196978f, 0.1215686f);
            var shirtColor = new Pixel(0.3254902f, 0.3803922f, 0.1058824f);
            var trouserColor = new Pixel(0.7568628f, 0.7098039f, 0.2980392f);

            Assert.AreEqual(faceColor, pixels[0]);
            Assert.AreEqual(hairColor, pixels[1]);
            Assert.AreEqual(shirtColor, pixels[2]);
            Assert.AreEqual(trouserColor, pixels[3]);
        }

        [TestMethod]
        public void TestShowQuadAt1_1()
        {
            var pixels = new[] { new Pixel(), new Pixel(), new Pixel(), new Pixel() };
            new Graphic
            {
                Gl = new GLWrapper(),
                Window = new GameWindow { Width = 200, Height = 200 },
                TextureFileName = @"..\..\Images\textures.png",
                DetermineScreenSizeInTiles = (SizeInPixels size) => new Vector2d(2, 2),
                EndRedrawAction = (Graphic graphic) =>
                {
                    pixels[0].ReadBuffer(150, 172);
                    pixels[1].ReadBuffer(145, 190);
                    pixels[2].ReadBuffer(152, 146);
                    pixels[3].ReadBuffer(160, 124);
                    graphic.Window.Close();
                },
                QuadList = new List<Quad> {new Quad { Position = new MapPosition(1, 1), FragmentPositionY = 3.0 }}
            }.Run();

            var faceColor = new Pixel(0.9058824f, 0.7882353f, 0.6196079f);
            var hairColor = new Pixel(0.627451f, 0.4196978f, 0.1215686f);
            var shirtColor = new Pixel(0.3254902f, 0.3803922f, 0.1058824f);
            var trouserColor = new Pixel(0.7568628f, 0.7098039f, 0.2980392f);

            Assert.AreEqual(faceColor, pixels[0]);
            Assert.AreEqual(hairColor, pixels[1]);
            Assert.AreEqual(shirtColor, pixels[2]);
            Assert.AreEqual(trouserColor, pixels[3]);
        }

        [TestMethod]
        public void TestShowTwoQuads()
        {
            var pixels = new[] { new Pixel(), new Pixel(), new Pixel(), new Pixel() };
            new Graphic
            {
                Gl = new GLWrapper(),
                Window = new GameWindow { Width = 200, Height = 200 },
                TextureFileName = @"..\..\Images\textures.png",
                DetermineScreenSizeInTiles = (SizeInPixels size) => new Vector2d(2, 2),
                EndRedrawAction = (Graphic graphic) =>
                {
                    pixels[0].ReadBuffer(47, 172);
                    pixels[1].ReadBuffer(47, 190);
                    pixels[2].ReadBuffer(147, 172);
                    pixels[3].ReadBuffer(147, 190);
                    graphic.Window.Close();
                },
                QuadList = new List<Quad>
                {
                    new Quad { Position = new MapPosition(1, 1), FragmentPositionY = 3.0 },
                    new Quad { Position = new MapPosition(0, 1), FragmentPositionY = 3.0 }
                }
            }.Run();

            var faceColor = new Pixel(0.9058824f, 0.7882353f, 0.6196079f);
            var hairColor = new Pixel(0.627451f, 0.4196978f, 0.1215686f);

            Assert.AreEqual(faceColor, pixels[0]);
            Assert.AreEqual(hairColor, pixels[1]);
            Assert.AreEqual(faceColor, pixels[2]);
            Assert.AreEqual(hairColor, pixels[3]);
        }

        [TestMethod]
        public void TestShowMap()
        {
            var pixels = new [] {new Pixel(), new Pixel(), new Pixel(), new Pixel()};
            var mapObject = new MapObject {Position = new MapPosition(0, 0)};
            var staticObject = new MapObject {Position = new MapPosition(1, 1)};

            var map = new Map();
            map.AddObject(mapObject);
            map.AddObject(staticObject);

            var graphicObject = new Graphic
            {
                Gl = new GLWrapper(),
                Window = new GameWindow {Width = 200, Height = 200},
                TextureFileName = @"..\..\Images\textures.png",
                DetermineScreenSizeInTiles = (SizeInPixels size) => new Vector2d(5, 5),
                EndRedrawAction = (Graphic graphic) =>
                {
                    pixels[0].ReadBuffer(18, 28);
                    pixels[1].ReadBuffer(18, 35);
                    pixels[2].ReadBuffer(59, 69);
                    pixels[3].ReadBuffer(58, 76);
                    graphic.Window.Close();
                },
                QuadList = map
            };
            graphicObject.Window.Run(1.0, 1.0);

            var faceColor = new Pixel(0.9058824f, 0.7882353f, 0.6196079f);
            var hairColor = new Pixel(0.627451f, 0.4196978f, 0.1215686f);
            
            Assert.AreEqual(faceColor, pixels[0]);
            Assert.AreEqual(hairColor, pixels[1]);
            Assert.AreEqual(faceColor, pixels[2]);
            Assert.AreEqual(hairColor, pixels[3]);
        }

        [TestMethod]
        public void MoveTestMoveWithKeyboard()
        {
            var window = new GameWindow {Width = 200, Height = 200};
            var charQuad = new Quad {FragmentPositionY = 3.0};

            var quadList = new List<Quad>();
            for (var x = 0; x < 10; x ++)
            {
                for (var y = 0; y < 10; y ++)
                {
                    quadList.Add(new Quad { FragmentPositionX = 1.0, FragmentPositionY = 3.0, Position = new MapPosition(x, y)});
                }
            }
            quadList.Add(new Quad { FragmentPositionX = 2.0, FragmentPositionY = 3.0, Position = new MapPosition(3, 2)});
            quadList.Add(new Quad { FragmentPositionX = 2.0, FragmentPositionY = 3.0, Position = new MapPosition(4, 7) });
            quadList.Add(new Quad { FragmentPositionX = 2.0, FragmentPositionY = 3.0, Position = new MapPosition(2, 5) });

            quadList.Add(charQuad);

            var graphicObject = new Graphic
            {
                Gl = new GLWrapper(),
                Window = window,
                TextureFileName = @"..\..\Images\textures.png",
                DetermineScreenSizeInTiles = (SizeInPixels size) => new Vector2d(10, 10),
                QuadList = quadList,
                EndRedrawAction = (Graphic graphic) =>
                {
                    //graphic.Window.Close();
                }
            };

            window.Keyboard.KeyDown += (sender, args) =>
            {
                switch (args.Key)
                {
                    case Key.Escape:
                        graphicObject.Window.Close();
                        break;

                    case Key.Right:
                        charQuad.Position = new MapPosition(charQuad.Position, PositionChange.CreateRight());
                        break;

                    case Key.Left:
                        charQuad.Position = new MapPosition(charQuad.Position, PositionChange.CreateLeft());
                        break;

                    case Key.Up:
                        charQuad.Position = new MapPosition(charQuad.Position, PositionChange.CreateUp());
                        break;

                    case Key.Down:
                        charQuad.Position = new MapPosition(charQuad.Position, PositionChange.CreateDown());
                        break;
                }
            };

            window.Run(5.0);
        }
    }
}
