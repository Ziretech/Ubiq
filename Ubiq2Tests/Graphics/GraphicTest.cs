﻿using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Ubiq2.GameLogic;
using Ubiq2.Graphics;
using Keyboard = Ubiq2.Control.Keyboard;
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
            var graphicObject = new Graphic
            {
                Gl = new GLWrapper(),
                Window = window,
                TextureFileName = @"..\..\Images\textures.png",
                DetermineScreenSizeInTiles = (SizeInPixels size) => new Vector2d(10, 10),
                QuadList = new List<Quad> {charQuad},
                EndRedrawAction = (Graphic graphic) =>
                {
                    //graphic.Window.Close();
                }
            };

            // Det fungerar inte bra att enbart kontrollera tangenter vid UpdateFrame:
            // Fungerar dåligt att ta "småsteg". Även ett mycket snabbt tryck (som inte
            // fångas upp inom någon UpdateFrame) bör räknas som ett steg.

            // Därför behöver man ha ett tangentbordsobjekt som kontrollerar när tangenter
            // trycks ner och släpps upp.
            
            // Generera action om en tangent har tryckts ner eller är nedtryckt. Om en tangent
            // inte längre är nedtryckt, återställ tangenten (avregistrera den som "har blivit
            // nedtryckt").

            // Det är ett objekt som skapas och har en koppling till window.Keyboard.
            // Den registerar sig på KeyDown för att veta om tangenten har tryckts ner sedan
            // förra återställningen.
            // Den har metoder för att nollställa tangenter.
            // Den har en metod för att meddela om en tangent är aktiv.

            window.TargetUpdateFrequency = 1.0;

            var keyboard = new Keyboard {InternalKeyboard = window.Keyboard};

            window.UpdateFrame += (sender, args) =>
            {
                if (keyboard.Pressed(Key.Escape))
                {
                    graphicObject.Window.Close();
                }
                if (keyboard.Pressed(Key.Right))
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange { X = 1 });
                }
                if (keyboard.Pressed(Key.Left))
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange { X = -1 });
                }
                if (keyboard.Pressed(Key.Up))
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange { Y = 1 });
                }
                if (keyboard.Pressed(Key.Down))
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange { Y = -1 });
                }

                keyboard.ClearAllKeys();

                /*if (window.Keyboard[Key.Escape])
                {
                    graphicObject.Window.Close();
                }
                if (window.Keyboard[Key.Right])
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange { X = 1 });
                }
                if (window.Keyboard[Key.Left])
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange { X = -1 });
                }
                if (window.Keyboard[Key.Up])
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange { Y = 1 });
                }
                if (window.Keyboard[Key.Down])
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange { Y = -1 });
                }*/
            };

            /*window.Keyboard.KeyDown += (sender, args) =>
            {
                if (window.Keyboard[Key.Escape])
                {
                    graphicObject.Window.Close();
                }
                if (window.Keyboard[Key.Right])
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange {X = 1});
                }
                if (window.Keyboard[Key.Left])
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange {X = -1});
                }
                if (window.Keyboard[Key.Up])
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange {Y = 1});
                }
                if (window.Keyboard[Key.Down])
                {
                    charQuad.Position = new MapPosition(charQuad.Position, new PositionChange {Y = -1});
                }
            };*/

            window.Run(5.0);
        }
    }
}
