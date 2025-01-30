using SFML.Graphics;
using System.Numerics;

namespace AGWAE
{
    internal class UIObject : GameObject
    {
        public Action? Clicked;

        public UIObject(Vector2 position, Quaternion rotation, Vector2 scale, Layer layer, Sprite sprite)
            : base(position, rotation, scale, layer, sprite)
        {
        }

        public UIObject(Vector2 position, Vector2 scale, Layer layer, Sprite sprite)
            : base(position, scale, layer, sprite)
        {
        }

        public UIObject(Vector2 position, Layer layer, Sprite sprite)
            : base(position, layer, sprite)
        {
        }
    }
}
