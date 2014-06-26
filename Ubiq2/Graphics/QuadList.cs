using System.Collections;
using System.Collections.Generic;

namespace Ubiq2.Graphics
{
    public class QuadList : IEnumerable<Quad>
    {
        private List<Quad> list = new List<Quad>();

        public void Add(Quad quad)
        {
            list.Add(quad);
        }

        public IEnumerator<Quad> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
