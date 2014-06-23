using System.Collections.Generic;
using OpenTK.Input;

namespace Ubiq2.Control
{
    public class Keyboard
    {
        private KeyboardDevice _internalKeyboard;
        public KeyboardDevice InternalKeyboard
        {
            get { return _internalKeyboard; }
            set
            {
                _internalKeyboard = value;
                _internalKeyboard.KeyDown += (sender, args) => _pressedKeys.Add(args.Key);
            }
        }
        private readonly List<Key> _pressedKeys = new List<Key>();

        public bool Pressed(Key key)
        {
            return InternalKeyboard[key] || _pressedKeys.Contains(key);
        }

        public void ClearAllKeys()
        {
            _pressedKeys.RemoveRange(0, _pressedKeys.Count);
        }
    }
}
