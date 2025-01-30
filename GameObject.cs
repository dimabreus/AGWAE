using SFML.Graphics;
using System.Numerics;

namespace AGWAE
{
    internal class GameObject(Vector2 position, Quaternion rotation, Vector2 scale, Layer layer, Sprite sprite)
    {
        public virtual Vector2 Position { get; set; } = position;
        public Quaternion Rotation { get; set; } = rotation;
        public Vector2 Scale { get; set; } = scale;
        public Layer Layer { get; set; } = layer;
        public Sprite Sprite { get; set; } = sprite;
        public Vector2 Size { get; } = new Vector2(sprite.TextureRect.Width, sprite.TextureRect.Height) * scale;

        public GameObject(Vector2 position, Vector2 scale, Layer layer, Sprite sprite)
            : this(position, Quaternion.Zero, scale, layer, sprite)
        {
        }

        public GameObject(Vector2 position, Layer layer, Sprite sprite)
            : this(position, Quaternion.Zero, Vector2.One, layer, sprite)
        {
        }

        public virtual void Draw(RenderWindow window)
        {
            Draw(window, Position);
        }

        public virtual void Draw(RenderWindow window, Vector2 position)
        {
            if (Sprite == null)
                return;

            Sprite.Position = new SFML.System.Vector2f(position.X, position.Y);

            float angle = MathF.Atan2(2 * (Rotation.W * Rotation.Z + Rotation.X * Rotation.Y),
                                      1 - 2 * (Rotation.Y * Rotation.Y + Rotation.Z * Rotation.Z)) * 180 / MathF.PI;
            Sprite.Rotation = angle;

            Sprite.Scale = new SFML.System.Vector2f(Scale.X, Scale.Y);

            window.Draw(Sprite);
        }
    }
}
