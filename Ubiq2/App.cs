using OpenTK;
using OpenTK.Input;
using Ubiq2.GameLogic;
using Ubiq2.Graphics;

namespace Ubiq2
{
    static class App
    {
        public static void Main()
        {
            var gameWindow = new GameWindow { Width = 512, Height = 512 };
            var map = new Map();

            for(var x=0; x<16; x++)
                for (var y = 0; y < 16; y++)
                {
                    map.AddObject(new MapObject
                    {
                        Blocking = false,
                        Position = new MapPosition(x, y),
                        Quad = new Quad { FragmentPositionX = 1.0, FragmentPositionY = 3.0 }
                    });
                }

            map.AddObject(new MapObject
            {
                Blocking = true,
                Position = new MapPosition(3, 2),
                Quad = new Quad { FragmentPositionX = 2.0, FragmentPositionY = 3.0}
            });

            map.AddObject(new MapObject
            {
                Blocking = true,
                Position = new MapPosition(4, 7),
                Quad = new Quad { FragmentPositionX = 2.0, FragmentPositionY = 3.0 }
            });

            map.AddObject(new MapObject
            {
                Blocking = true,
                Position = new MapPosition(2, 5),
                Quad = new Quad { FragmentPositionX = 2.0, FragmentPositionY = 3.0 }
            });

            var character = new MapObject {Blocking = true, Position = new MapPosition(0, 0)};
            map.AddObject(character);
            
            var graphicObject = new Graphic
            {
                Gl = new GLWrapper(),
                Window = gameWindow,
                TextureFileName = @"..\..\Images\textures.png",
                DetermineScreenSizeInTiles = size => new Vector2d(16, 16),
                QuadList = map
            };

            gameWindow.Keyboard.KeyDown += (sender, args) =>
            {
                switch (args.Key)
                {
                    case Key.Escape:
                        graphicObject.Window.Close();
                        break;

                    case Key.Right:
                        map.Move(character, PositionChange.CreateRight());
                        break;

                    case Key.Left:
                        map.Move(character, PositionChange.CreateLeft());
                        break;

                    case Key.Up:
                        map.Move(character, PositionChange.CreateUp());
                        break;

                    case Key.Down:
                        map.Move(character, PositionChange.CreateDown());
                        break;
                }
            };

            graphicObject.Run();
        }
    }
}
