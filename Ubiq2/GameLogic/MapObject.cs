
using Ubiq2.Graphics;

namespace Ubiq2.GameLogic
{

    public class MapObject : IDrawable
    {
        private Quad _quad;

        public Quad Quad
        {
            get { return _quad; }
            set
            {
                MapPosition oldPosition = Position;
                _quad = value;
                if (oldPosition != null && _quad != null)
                {
                    _quad.Position = new MapPosition(oldPosition);
                }
            }
        }

        private MapPosition _position;
        public MapPosition Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_position != null && Quad != null)
                {
                    Quad.Position = _position;
                }
            }
        }
        public bool Blocking { get; set; }

        public MapObject()
        {
            Blocking = true;
            Position = new MapPosition(0, 0);
            Quad = new Quad { FragmentPositionY = 3.0 };
        }

        public Quad GetQuad()
        {
            return Quad;
        }
    }
}
