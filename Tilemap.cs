using System.Numerics;

namespace AGWAE
{
    internal class Tilemap(Func<GameObject>[] gameObjectVariants, Vector2 position)
    {
        public Dictionary<Vector2, GameObject> GameObjects { get; private set; } = new();

        private readonly Func<GameObject>[] gameObjectVariants = gameObjectVariants;
        private Vector2 position = position;

        public Tilemap(Func<GameObject>[] gameObjectVariants)
            : this(gameObjectVariants, Vector2.Zero)
        {
        }

        public void SetAt(int x, int y, int gameObjectVariant)
        {
            Vector2 key = new(x, y);

            GameObjects.Remove(key);

            GameObject gameObject = gameObjectVariants[gameObjectVariant]();
            gameObject.Position = position + new Vector2(x, y) * gameObject.Size;

            GameObjects[key] = gameObject;
        }
    }
}
