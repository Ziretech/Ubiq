using System.Collections;
using System.Collections.Generic;
using Ubiq2.Graphics;

namespace Ubiq2.GameLogic
{
    public class MapObjectEnumerator : IEnumerator<MapObject>, IEnumerator<Quad>
    {
        private readonly IEnumerator<MapObject> _enumerator;

        public MapObjectEnumerator(IEnumerator<MapObject> enumerator)
        {
            _enumerator = enumerator;
        }

        public void Dispose()
        {
            _enumerator.Dispose();
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }

        Quad IEnumerator<Quad>.Current
        {
            get { return _enumerator.Current.GetQuad(); }
        }

        public MapObject Current
        {
            get { return _enumerator.Current; }
        }

        object IEnumerator.Current
        {
            get { return _enumerator.Current; }
        }
    }
}
