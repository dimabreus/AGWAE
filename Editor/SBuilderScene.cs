using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Numerics;

namespace AGWAE.Editor
{
    internal class SBuilderScene : GameScene
    {
        public override string Name { get; protected set; } = "Builder";

        protected override List<GameObject> GameObjects { get; }

        private readonly Vector2 spriteSize;

        private readonly List<UIObject> selector;
        private int currentSpriteId = 0;

        private Tilemap tilemap;

        private bool isPlacing = false;

        private Vector2 currentOffset = Vector2.Zero;
        private Vector2 totalOffset = new(Config.WINDOW_SIZE_X / 2, Config.WINDOW_SIZE_Y / 2);
        private Vector2 dragMovingStartPosition = Vector2.Zero;
        private bool isMoving = false;

        private bool isEraserKeyPressed = false;
        private int previousSpriteId = 0;

        private List<Func<GameObject>> tilemapGameObjects;

        public SBuilderScene()
        {
            List<Sprite> tilemapSprites = [
                Sprites.SingleLeft,
                Sprites.Single,
                Sprites.SingleRight,
                Sprites.TopLeft,
                Sprites.Top,
                Sprites.TopRight,
                Sprites.MiddleLeft,
                Sprites.Middle,
                Sprites.MiddleRight,
                Sprites.BottomLeft,
                Sprites.Bottom,
                Sprites.BottomRight,
                Sprites.SlopeLeft,
                Sprites.SlopeRight,
                Sprites.SlopeUnderLeft,
                Sprites.SlopeUnderRight,
                Sprites.SlopeUnderLeftWithoutBorder,
                Sprites.SlopeUnderRightWithoutBorder,
                Sprites.SingleBoth
            ];

            tilemapGameObjects = [];

            foreach (var sprite in tilemapSprites)
            {
                tilemapGameObjects.Add(() => new GameObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(sprite)));
            }

            tilemap = new Tilemap([.. tilemapGameObjects]);

            spriteSize = tilemapGameObjects.First()().Size;

            selector = [];

            for (int i = 0; i < tilemapSprites.Count; i++)
            {
                var newI = i;
                UIObject spriteButton = new(new Vector2(i * spriteSize.X, 0), Vector2.One * 5, Layer.UI, new(tilemapSprites[i]));
                spriteButton.Clicked += () => currentSpriteId = newI;
                selector.Add(spriteButton);
            }

            GameObjects = [.. selector, .. tilemap.GameObjects.Values];
        }
        public override void Update(HashSet<Keyboard.Key> pressedKeys)
        {
            base.Update(pressedKeys);

            if ((pressedKeys.Contains(Keyboard.Key.LControl) || pressedKeys.Contains(Keyboard.Key.RControl)) && pressedKeys.Contains(Keyboard.Key.S))
                Save();
            if ((pressedKeys.Contains(Keyboard.Key.LControl) || pressedKeys.Contains(Keyboard.Key.RControl)) && pressedKeys.Contains(Keyboard.Key.O))
                Open();
            if (pressedKeys.Contains(Keyboard.Key.Q) && !isEraserKeyPressed)
            {
                previousSpriteId = currentSpriteId == -1 ? previousSpriteId : currentSpriteId;
                currentSpriteId = currentSpriteId == -1 ? previousSpriteId : -1;
            }
            isEraserKeyPressed = pressedKeys.Contains(Keyboard.Key.Q);
        }

        public override void HandleClick(MouseButtonEventArgs e)
        {
            base.HandleClick(e);

            if (e.Button == Mouse.Button.Middle)
            {
                isMoving = true;
                dragMovingStartPosition = new Vector2(e.X, e.Y);
            }

            if (e.Button == Mouse.Button.Left)
            {
                isPlacing = true;

                ContinuePlacing(e.X, e.Y);
            }
        }
        public override void HandleMouseMoved(MouseMoveEventArgs e)
        {
            base.HandleMouseMoved(e);

            if (isPlacing)
            {
                ContinuePlacing(e.X, e.Y);
            }

            if (isMoving)
            {
                Vector2 offset = new Vector2(e.X, e.Y) - dragMovingStartPosition;
                currentOffset = offset;
            }
        }

        public override void HandleClickRelease(MouseButtonEventArgs e)
        {
            base.HandleClickRelease(e);

            if (e.Button == Mouse.Button.Left)
            {
                isPlacing = false;
            }

            if (e.Button == Mouse.Button.Middle)
            {
                isMoving = false;

                totalOffset += currentOffset;

                currentOffset = Vector2.Zero;
                dragMovingStartPosition = Vector2.Zero;
            }
        }

        public override void Draw(RenderWindow window, GameObject gameObject)
        {
            if (gameObject is UIObject)
            {
                gameObject.Draw(window);

                return;
            }

            gameObject.Draw(window, gameObject.Position + totalOffset + currentOffset);
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);

            DrawCenter(window);
            DrawOutlineSelected(window);
        }

        private void ContinuePlacing(int x, int y)
        {
            if (!IsUIClick(x, y))
            {
                Vector2i coords = GetBlocksCoords(x, y);

                if (currentSpriteId == -1)
                    RemoveBlockAt(coords.X, coords.Y);
                else
                    PlaceBlockAt(coords.X, coords.Y);
            }
        }

        private void DrawCenter(RenderWindow window)
        {
            Vector2 position = new Vector2(0f, 0f) + totalOffset + currentOffset;

            var sphere = new CircleShape(5f) { Position = new Vector2f(position.X, position.Y) };

            window.Draw(sphere);
        }

        private void DrawOutlineSelected(RenderWindow window)
        {
            if (currentSpriteId == -1) return;

            UIObject selectedSprite = selector[currentSpriteId];

            var outline = new RectangleShape(new Vector2f(selectedSprite.Size.X, selectedSprite.Size.Y))
            {
                Position = new Vector2f(selectedSprite.Position.X, selectedSprite.Position.Y),
                OutlineColor = Color.White,
                OutlineThickness = 2,
                FillColor = Color.Transparent,
            };

            window.Draw(outline);
        }

        private void Save()
        {
            string filepath = GetPathToSave();

            tilemap.Export(filepath);

            Console.WriteLine($"File successfully saved: {filepath}");
        }
        private void Open()
        {
            string filepath = GetPathToLoad();

            tilemap = new Tilemap([.. tilemapGameObjects], filepath);
            UpdateGameObjects();
        }

        private static string GetPathToSave()
        {
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "level.json");

            Console.Write($"Enter filepath for save (By default: {defaultPath}): ");
            string? inputPath = Console.ReadLine()?.Trim();
            string filePath = string.IsNullOrEmpty(inputPath) ? defaultPath : inputPath;

            return filePath;
        }

        private static string GetPathToLoad()
        {
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "level.json");

            Console.Write($"Enter filepath for open (By default: {defaultPath}): ");

            string? inputPath = Console.ReadLine()?.Trim();
            string filePath = string.IsNullOrEmpty(inputPath) ? defaultPath : inputPath;

            return filePath!;
        }
        private void UpdateGameObjects()
        {
            GameObjects.Clear();
            GameObjects.AddRange(tilemap.GameObjects.Values);
            GameObjects.AddRange(selector);
        }

        private bool IsUIClick(int x, int y)
        {
            return selector.Any((uiObject) => new FloatRect(uiObject.Position.X, uiObject.Position.Y, uiObject.Size.X, uiObject.Size.Y).Contains(x, y));
        }

        private Vector2i GetBlocksCoords(int x, int y)
        {
            Vector2 actualCoords = new Vector2(x, y) - totalOffset - currentOffset;

            Vector2 coords = new(actualCoords.X / spriteSize.X, actualCoords.Y / spriteSize.Y);

            return new Vector2i((int)Math.Floor(coords.X), (int)Math.Floor(coords.Y));
        }

        private void PlaceBlockAt(int x, int y)
        {
            tilemap.SetAt(x, y, currentSpriteId);
            UpdateGameObjects();
        }

        private void RemoveBlockAt(int x, int y)
        {
            tilemap.RemoveAt(x, y);
            UpdateGameObjects();
        }
    }
}
