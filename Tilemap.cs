using System.Numerics;
using System.Text.Json;

namespace AGWAE
{
    internal class Tilemap(Func<GameObject>[] gameObjectVariants, Vector2 position)
    {
        public Dictionary<Vector2, GameObject> GameObjects { get; private set; } = [];
        private readonly Dictionary<Vector2, int> Grid = [];

        private readonly Func<GameObject>[] gameObjectVariants = gameObjectVariants;
        private Vector2 position = position;

        public Tilemap(Func<GameObject>[] gameObjectVariants)
            : this(gameObjectVariants, Vector2.Zero)
        {
        }

        public Tilemap(Func<GameObject>[] gameObjectVariants, string filepath)
            : this(gameObjectVariants)
        {
            LoadGridFromFile(filepath);

            foreach (var el in Grid)
            {
                GameObject gameObject = gameObjectVariants[el.Value]();
                gameObject.Position = position + el.Key * gameObject.Size;

                GameObjects[el.Key] = gameObject;
            }

        }

        public void SetAt(int x, int y, int gameObjectVariant)
        {
            Vector2 key = new(x, y);

            GameObject gameObject = gameObjectVariants[gameObjectVariant]();
            gameObject.Position = position + new Vector2(x, y) * gameObject.Size;

            GameObjects[key] = gameObject;
            Grid[key] = gameObjectVariant;
        }

        public void RemoveAt(int x, int y)
        {
            Vector2 key = new(x, y);

            GameObjects.Remove(key);
            Grid.Remove(key);
        }

        public void Export(string filePath)
        {
            var serializedGrid = new Dictionary<string, int>();
            foreach (var kvp in Grid)
            {
                serializedGrid[$"({kvp.Key.X},{kvp.Key.Y})"] = kvp.Value;
            }

            JsonSerializerOptions options = new() { WriteIndented = true };
            string json = JsonSerializer.Serialize(serializedGrid, options);
            File.WriteAllText(filePath, json);
        }

        private void LoadGridFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var serializedGrid = JsonSerializer.Deserialize<Dictionary<string, int>>(json);

            foreach (var kvp in serializedGrid)
            {
                var parts = kvp.Key.Trim('(', ')').Split(',');
                if (parts.Length == 2 &&
                    float.TryParse(parts[0], out float x) &&
                    float.TryParse(parts[1], out float y))
                {
                    Grid[new Vector2(x, y)] = kvp.Value;
                }
            }
        }
    }
}
