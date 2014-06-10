namespace Ubiq2.GameLogic
{
    public class Teleporter : MapObject
    {
        public MapPosition Target { get; set; }

        public Teleporter()
        {
            Target = new MapPosition(0, 0);
        }

        public void HandleWalkInteraction(MapObject mapObject)
        {
            mapObject.Position = new MapPosition(Target);
        }
    }
}
