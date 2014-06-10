using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Ubiq2.Graphics
{
    public class Graphic
    {
        public IGL Gl { get; set; }

        public delegate Vector2d DetermineScreenSizeInTilesDelegate(SizeInPixels size);
        public DetermineScreenSizeInTilesDelegate DetermineScreenSizeInTiles { get; set; }

        private GameWindow _window;
        public GameWindow Window
        {
            get
            {
                return _window;
            }
            set
            {
                _window = value;
                _window.Load += Load;
                _window.Resize += Resize;
                _window.RenderFrame += RenderFrame;
            }
        }

        public string TextureFileName { get; set; }

        public delegate void EndRedrawActionDelegate(Graphic graphic);
        public EndRedrawActionDelegate EndRedrawAction { get; set; }
        public IEnumerable<Quad> QuadList { get; set; }

        public void Run()
        {
            Window.Run();
        }

        private void Load(object sender, EventArgs e)
        {
            Gl.ReadBuffer(ReadBufferMode.Front);

            int textureId = Gl.GenTexture();
            InitTransparentTexturing();
            InitTileTexture(textureId, TextureFileName);
            ActivateTileTexture(textureId, new Vector3d(1.0 / 4.0, -1.0 / 4.0, 1.0));
        }

        private void InitTransparentTexturing()
        {
            Gl.Enable(EnableCap.Texture2D);
            Gl.Enable(EnableCap.Blend);
            Gl.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        private void InitTileTexture(int textureId, string filename)
        {
            Gl.BindTexture(TextureTarget.Texture2D, textureId);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Nearest);
            var bitmap = new Bitmap(filename);
            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Gl.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height,
                0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);
        }

        private void ActivateTileTexture(int textureId, Vector3d textureScale)
        {
            Gl.BindTexture(TextureTarget.Texture2D, textureId);
            Gl.MatrixMode(MatrixMode.Texture);
            Gl.LoadIdentity();
            Gl.Scale(textureScale);
            Gl.MatrixMode(MatrixMode.Modelview);
        }

        private void Resize(object sender, EventArgs e)
        {
            var screenSizeInTiles = DetermineScreenSizeInTiles(new SizeInPixels(Window.Width, Window.Height));//new Vector2d(ScreenSize.Width, ScreenSize.Height);

            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadIdentity();
            Gl.Ortho(0.0, screenSizeInTiles.X, 0.0, screenSizeInTiles.Y, 0.0, 4.0);
            Gl.MatrixMode(MatrixMode.Modelview);
            Gl.Viewport(0, 0, Window.Width, Window.Height);
        }

        private void RenderFrame(object sender, FrameEventArgs e)
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit);

            if (QuadList != null)
            {
                foreach (var quad in QuadList)
                {
                    quad.Render(Gl);
                }
            }
            
            Window.SwapBuffers();

            if (EndRedrawAction != null)
            {
                EndRedrawAction(this);
            }
        }

        
    }
}
