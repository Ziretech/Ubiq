using System;
using System.Collections;
using System.Collections.Generic;
using Ubiq2.Graphics;

namespace Ubiq2.GameLogic
{
    public class Map : IEnumerable<Quad>
    {
        private readonly List<MapObject> _mapObjects = new List<MapObject>();

        public void AddObject(MapObject mapObject)
        {
            if(mapObject == null)
                throw new ArgumentNullException();

            _mapObjects.Add(mapObject);
        }

        public IDrawable GetDrawable(int characterIndex)
        {
            return _mapObjects[characterIndex];
        }

        public int GetNumDrawables()
        {
            return _mapObjects.Count;
        }

        public void Move(MapObject moveableObject, PositionChange direction)
        {
            var newPosition = new MapPosition(moveableObject.Position, direction);
            var noStaticObject = _mapObjects.TrueForAll(staticObject => !staticObject.Position.Equals(newPosition) || !staticObject.Blocking);
            if (noStaticObject)
            {
                moveableObject.Position = newPosition;
            }
        }

        public IEnumerator<Quad> GetEnumerator()
        {
            return new MapObjectEnumerator(_mapObjects.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
