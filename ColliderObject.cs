using SFML.Graphics;
using System.Numerics;

namespace AGWAE
{

    internal class ColliderObject : GameObject
    {
        public Physics.Shape Collider;

        private Vector2 colliderPosition;

        private readonly Physics.Shape initialCollider;
        private readonly Vector2 colliderInitialPosition;

        public ColliderObject(Vector2 position, Quaternion rotation, Vector2 scale, Layer layer, Sprite sprite, Physics.Shape collider)
            : base(position, rotation, scale, layer, sprite)
        {
            Collider = collider;
            colliderPosition = position;
            initialCollider = new Physics.Shape(Collider.Vertices);
            colliderInitialPosition = new(position.X, position.Y);
        }

        public ColliderObject(Vector2 position, Quaternion rotation, Vector2 scale, Layer layer, Sprite sprite)
            : base(position, rotation, scale, layer, sprite)
        {
            FloatRect rect = new(position.X, position.Y, Size.X, Size.Y);
            Collider = new Physics.Shape(rect);
            colliderPosition = position;
            initialCollider = new Physics.Shape(Collider.Vertices);
            colliderInitialPosition = new(position.X, position.Y);
        }

        public ColliderObject(Vector2 position, Vector2 scale, Layer layer, Sprite sprite)
            : this(position, Quaternion.Zero, scale, layer, sprite)
        {
        }

        public ColliderObject(Vector2 position, Vector2 scale, Layer layer, Sprite sprite, Physics.Shape collider)
            : this(position, Quaternion.Zero, scale, layer, sprite, collider)
        {
        }

        public ColliderObject(Vector2 position, Layer layer, Sprite sprite)
            : this(position, Quaternion.Zero, Vector2.One, layer, sprite)
        {
        }

        public ColliderObject(Vector2 position, Layer layer, Sprite sprite, Physics.Shape collider)
            : this(position, Quaternion.Zero, Vector2.One, layer, sprite, collider)
        {
        }

        public override Vector2 Position
        {
            get => base.Position;
            set
            {
                base.Position = value;
                UpdateCollider();
            }
        }

        private void UpdateCollider()
        {
            if (Collider != null)
            {
                Vector2 offset = Position - colliderPosition;

                Collider = new Physics.Shape(Collider.Vertices.Select(vertex => vertex + offset).ToArray());
                colliderPosition = Position;
            }
        }

        public override void Draw(RenderWindow window, Vector2 position)
        {
            base.Draw(window, position);

            if (!Config.DEBUG_COLLISION) return;

            Vector2 offset = position - colliderInitialPosition;

            for (int i = 0; i < initialCollider.Vertices.Length; i++)
            {
                Vector2 pointA = initialCollider.Vertices[i] + offset;
                Vector2 pointB = initialCollider.Vertices[i + 1 >= initialCollider.Vertices.Length ? 0 : i + 1] + offset;

                var line = new VertexArray(PrimitiveType.Lines, 2);
                line[0] = new Vertex(new SFML.System.Vector2f(pointA.X, pointA.Y));
                line[1] = new Vertex(new SFML.System.Vector2f(pointB.X, pointB.Y));

                window.Draw(line);
            }
        }
    }
}