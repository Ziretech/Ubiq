using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Ubiq2Tests.Graphics
{
    [TestClass]
    public class GLTest
    {
        private GameWindow _window;

        [TestMethod]
        public void TestClear()
        {
            var pixel = new Pixel();
            _window = new GameWindow();
            _window.RenderFrame += (caller, args) =>
            {
                GL.ReadBuffer(ReadBufferMode.Front);
                GL.ClearColor(0.4f, 0.2f, 1.0f, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit);
                _window.SwapBuffers();
                pixel.ReadBuffer(0, 0);
                _window.Close();
            };
            _window.Run();
            Assert.AreEqual(new Pixel(0.4f, 0.2f, 1.0f), pixel);
        }

        [TestMethod]
        public void TestDrawQuad()
        {
            var pixel = new Pixel();
            _window = new GameWindow {Width = 200, Height = 200};
            _window.RenderFrame += (caller, args) =>
            {
                GL.ReadBuffer(ReadBufferMode.Front);
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Begin(PrimitiveType.Quads);
                {
                    GL.Color3(1.0f, 1.0f, 1.0f);
                    GL.Vertex2(0.0f, 0.0f);
                    GL.Vertex2(-1.0f, 0.0f);
                    GL.Vertex2(-1.0f, -1.0f);
                    GL.Vertex2(0.0f, -1.0f);
                }
                GL.End();
                _window.SwapBuffers();
                pixel.ReadBuffer(0, 0);
                _window.Close();
            };
            _window.Run();
            Assert.AreEqual(new Pixel(1.0f, 1.0f, 1.0f), pixel);
        }

        [TestMethod]
        public void TestOrtho()
        {
            var pixels = new[] {new Pixel(), new Pixel(), new Pixel(), new Pixel()};
            _window = new GameWindow {Width = 200, Height = 200};
            _window.RenderFrame += (caller, args) =>
            {
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0.0, 2.0, 0.0, 2.0, 0.0, 4.0);
                GL.Viewport(0, 0, 200, 200);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.ReadBuffer(ReadBufferMode.Front);
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Begin(PrimitiveType.Quads);
                {
                    GL.Color3(1.0f, 1.0f, 1.0f);
                    GL.Vertex2(0.0f, 0.0f);
                    GL.Vertex2(1.0f, 0.0f);
                    GL.Vertex2(1.0f, 1.0f);
                    GL.Vertex2(0.0f, 1.0f);
                }
                GL.End();
                _window.SwapBuffers();
                pixels[0].ReadBuffer(99, 99);
                pixels[1].ReadBuffer(99, 100);
                pixels[2].ReadBuffer(100, 100);
                pixels[3].ReadBuffer(100, 99);
                _window.Close();
            };
            _window.Run();
            Assert.AreEqual(new Pixel(1.0f, 1.0f, 1.0f), pixels[0]);
            Assert.AreEqual(new Pixel(0.0f, 0.0f, 0.0f), pixels[1]);
            Assert.AreEqual(new Pixel(0.0f, 0.0f, 0.0f), pixels[2]);
            Assert.AreEqual(new Pixel(0.0f, 0.0f, 0.0f), pixels[3]);
        }

        [TestMethod]
        public void TestDrawTexture()
        {
            var pixels = new[] { new Pixel(), new Pixel(), new Pixel(), new Pixel() };
            _window = new GameWindow { Width = 200, Height = 200 };
            _window.RenderFrame += (caller, args) =>
            {
                GL.Viewport(0, 0, 200, 200);
                GL.ReadBuffer(ReadBufferMode.Front);

                GL.Enable(EnableCap.Texture2D);
                int textureId = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, textureId);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                var bitmap = new Bitmap(@"..\..\Images\testtexture.png");
                var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                  ImageLockMode.ReadOnly,
                                                  System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height,
                    0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);

                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Begin(PrimitiveType.Quads);
                {
                    GL.TexCoord2(0.0f, 0.0f);
                    GL.Vertex2(-1.0f, -1.0f);
                    GL.TexCoord2(1.0f, 0.0f);
                    GL.Vertex2(1.0f, -1.0f);
                    GL.TexCoord2(1.0f, 1.0f);
                    GL.Vertex2(1.0f, 1.0f);
                    GL.TexCoord2(0.0f, 1.0f);
                    GL.Vertex2(-1.0f, 1.0f);
                }
                GL.End();
                _window.SwapBuffers();
                pixels[0].ReadBuffer(99, 99);
                pixels[1].ReadBuffer(99, 100);
                pixels[2].ReadBuffer(100, 100);
                pixels[3].ReadBuffer(100, 99);
                _window.Close();
            };
            _window.Run();
            Assert.AreEqual(new Pixel(1.0f, 0.0f, 0.0f), pixels[0]);
            Assert.AreEqual(new Pixel(0.0f, 1.0f, 0.0f), pixels[1]);
            Assert.AreEqual(new Pixel(0.0f, 0.0f, 1.0f), pixels[2]);
            Assert.AreEqual(new Pixel(0.0f, 0.0f, 0.0f), pixels[3]);
        }

        [TestMethod]
        public void TestDrawTransparentTexture()
        {
            var pixels = new[] { new Pixel(), new Pixel(), new Pixel(), new Pixel() };
            _window = new GameWindow { Width = 200, Height = 200 };
            _window.RenderFrame += (caller, args) =>
            {
                GL.Viewport(0, 0, 200, 200);
                GL.ReadBuffer(ReadBufferMode.Front);

                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.ClearColor(1.0f, 1.0f, 0.0f, 1.0f);
                int textureId = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, textureId);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                var bitmap = new Bitmap(@"..\..\Images\testtexture.png");
                var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                  ImageLockMode.ReadOnly,
                                                  System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height,
                    0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);

                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Begin(PrimitiveType.Quads);
                {
                    GL.TexCoord2(0.0f, 0.0f);
                    GL.Vertex2(-1.0f, -1.0f);
                    GL.TexCoord2(1.0f, 0.0f);
                    GL.Vertex2(1.0f, -1.0f);
                    GL.TexCoord2(1.0f, 1.0f);
                    GL.Vertex2(1.0f, 1.0f);
                    GL.TexCoord2(0.0f, 1.0f);
                    GL.Vertex2(-1.0f, 1.0f);
                }
                GL.End();
                _window.SwapBuffers();
                pixels[0].ReadBuffer(99, 99);
                pixels[1].ReadBuffer(99, 100);
                pixels[2].ReadBuffer(100, 100);
                pixels[3].ReadBuffer(100, 99);
                _window.Close();
            };
            _window.Run();
            Assert.AreEqual(new Pixel(1.0f, 0.0f, 0.0f), pixels[0]);
            Assert.AreEqual(new Pixel(0.0f, 1.0f, 0.0f), pixels[1]);
            Assert.AreEqual(new Pixel(0.0f, 0.0f, 1.0f), pixels[2]);
            Assert.AreEqual(new Pixel(1.0f, 1.0f, 0.0f), pixels[3]);
        }

        [TestMethod]
        public void TestSetModelViewMatrix()
        {
            var pixels = new[] { new Pixel(), new Pixel(), new Pixel(), new Pixel() };
            _window = new GameWindow { Width = 200, Height = 200 };
            _window.RenderFrame += (caller, args) =>
            {
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0.0, 2.0, 0.0, 2.0, 0.0, 4.0);
                GL.Viewport(0, 0, 200, 200);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.ReadBuffer(ReadBufferMode.Front);
                GL.Clear(ClearBufferMask.ColorBufferBit);
                GL.Color3(1.0f, 1.0f, 1.0f);
                DrawQuad();
                GL.PushMatrix();
                    GL.Translate(0.0f, 1.0f, 0.0f);
                    GL.Color3(1.0f, 0.0f, 0.0f);
                    DrawQuad();
                GL.PopMatrix();
                GL.PushMatrix();
                    GL.Translate(1.0f, 1.0f, 0.0f);
                    GL.Color3(0.0f, 1.0f, 0.0f);
                    DrawQuad();
                GL.PopMatrix();
                GL.PushMatrix();
                    GL.Translate(1.0f, 0.0f, 0.0f);
                    GL.Color3(0.0f, 0.0f, 1.0f);
                    DrawQuad();
                GL.PopMatrix();
                _window.SwapBuffers();
                pixels[0].ReadBuffer(99, 99);
                pixels[1].ReadBuffer(99, 100);
                pixels[2].ReadBuffer(100, 100);
                pixels[3].ReadBuffer(100, 99);
                _window.Close();
            };
            _window.Run();
            Assert.AreEqual(new Pixel(1.0f, 1.0f, 1.0f), pixels[0]);
            Assert.AreEqual(new Pixel(1.0f, 0.0f, 0.0f), pixels[1]);
            Assert.AreEqual(new Pixel(0.0f, 1.0f, 0.0f), pixels[2]);
            Assert.AreEqual(new Pixel(0.0f, 0.0f, 1.0f), pixels[3]);
        }

        private void DrawQuad()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Vertex2(0.0f, 0.0f);
            GL.Vertex2(1.0f, 0.0f);
            GL.Vertex2(1.0f, 1.0f);
            GL.Vertex2(0.0f, 1.0f);

            GL.End();
        }
    }
}
