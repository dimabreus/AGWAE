using SFML.Graphics;
using SFML.Window;

namespace AGWAE
{
    internal abstract class GameScene : Scene
    {
        protected abstract List<GameObject> GameObjects { get; }

        public override void Start()
        {
            base.Start();

            GameObjects.Sort((a, b) => a.Layer.CompareTo(b.Layer));
        }

        public override void Update(HashSet<Keyboard.Key> pressedKeys)
        {
            base.Update(pressedKeys);

            var colliders = GameObjects.OfType<ColliderObject>().ToList();
            foreach (var rb in GameObjects.OfType<RigidbodyObject>())
            {
                rb.UpdatePhysics(colliders);
            }
        }

        public override void Draw(RenderWindow window)
        {
            foreach (var gameObject in GameObjects)
            {
                Draw(window, gameObject);
            }
        }

        public abstract void Draw(RenderWindow window, GameObject gameObject);

        public override void HandleClick(MouseButtonEventArgs e)
        {
            base.HandleClick(e);

            var x = e.X;
            var y = e.Y;

            foreach (var gameObject in GameObjects)
            {
                if (gameObject is not UIObject uiObject) continue;

                var startX = uiObject.Position.X;
                var startY = uiObject.Position.Y;
                var endX = uiObject.Position.X + uiObject.Scale.X * uiObject.Sprite.TextureRect.Width;
                var endY = uiObject.Position.Y + uiObject.Scale.Y * uiObject.Sprite.TextureRect.Height;

                if (
                    (x >= startX && x <= endX) &&
                    (y >= startY && y <= endY)
                    )
                {
                    uiObject.Clicked?.Invoke();
                }
            }
        }
    }
}
