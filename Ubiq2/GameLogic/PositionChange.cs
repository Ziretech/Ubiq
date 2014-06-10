namespace Ubiq2.GameLogic
{
    public class PositionChange
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null && Equals(obj as PositionChange);
        }

        public bool Equals(PositionChange change)
        {
            return change != null && change.X == X && change.Y == Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }
}
