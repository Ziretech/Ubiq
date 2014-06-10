
using Ubiq2.Graphics;

namespace Ubiq2.GameLogic
{

    public class MapObject : IDrawable
    {
        private Quad _quad;
        private MapPosition _position;
        public MapPosition Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_position != null && _quad != null)
                {
                    _quad.Position = _position;

                }
            }
        }
        public bool Blocking { get; set; }

        public MapObject()
        {
            Blocking = true;
            Position = new MapPosition(0, 0);
            _quad = new Quad { FragmentPositionY = 3.0 };
        }

        public Quad GetQuad()
        {
            return _quad;
        }
    }
}
