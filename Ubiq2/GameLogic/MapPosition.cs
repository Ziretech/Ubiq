namespace Ubiq2.GameLogic
{
    public class MapPosition
    {
        public readonly int X;
        public readonly int Y;

        public MapPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public MapPosition(MapPosition position)
        {
            X = position.X;
            Y = position.Y;
        }

        public MapPosition(MapPosition position, PositionChange change)
        {
            X = position.X + change.X;
            Y = position.Y + change.Y;
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as MapPosition);
        }

        public bool Equals(MapPosition position)
        {
            return position != null && X == position.X && Y == position.Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", X, Y);
        }
    }
}
