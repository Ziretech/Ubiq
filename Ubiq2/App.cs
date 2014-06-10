using System.Collections.Generic;
using OpenTK;
using Ubiq2.GameLogic;
using Ubiq2.Graphics;

namespace Ubiq2
{
    static class App
    {
        public static void Main()
        {
            var graphics = new Graphic
            {
                Gl = new GLWrapper(),
                Window = new GameWindow { Width = 512, Height = 512 },
                TextureFileName = @"..\..\Images\textures.png",
                DetermineScreenSizeInTiles = (SizeInPixels size) => new Vector2d(16, 16),
                QuadList = new List<Quad> {new Quad {FragmentPositionY = 3.0, Position = new MapPosition(1, 4)}}
            };

            graphics.Run();
        }
    }
}
