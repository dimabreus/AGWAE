using SFML.Window;
using System.Numerics;

namespace AGWAE.Scenes
{
    internal class SGameScene : GameScene
    {
        public override string Name { get; protected set; } = "Game!";
        protected Camera Camera { get; }
        protected override List<GameObject> GameObjects { get; }

        private readonly RigidbodyObject player = new(new(500, 0), new(10, 10), Layer.Player, new(new SFML.Graphics.Texture("Assets/snowflake.png"))) { Mass = 10f };
        private readonly UIObject mainMenuButton = new(new(0, 0), new(5, 5), Layer.UI, new(Sprites.Home));

        public SGameScene()
        {
            mainMenuButton.Clicked += () => SceneManager.LoadScene(0);

            Func<GameObject>[] tilemapObjects = [
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.SingleLeft)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.Single)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.SingleRight)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.TopLeft)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.Top)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.TopRight)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.MiddleLeft)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.Middle)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.MiddleRight)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.BottomLeft)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.Bottom)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.BottomRight)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.SlopeLeft), new Physics.Shape([new Vector2(16 * 5, 0), new Vector2(16 * 5, 16 * 5), new Vector2(0, 16 * 5)])),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.SlopeRight), new Physics.Shape([new Vector2(0, 0), new Vector2(16 * 5, 16 * 5), new Vector2(0, 16 * 5)])),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.SlopeUnderLeft)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.SlopeUnderRight)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.SlopeUnderLeftWithoutBorder)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.SlopeUnderRightWithoutBorder)),
                () => new ColliderObject(Vector2.Zero, Vector2.One * 5, Layer.Ground, new(Sprites.SingleBoth))
                ];

            Tilemap tilemap = new(tilemapObjects, new(0, 250));

            tilemap.SetAt(0, 0, 6);
            tilemap.SetAt(1, 0, 8);
            tilemap.SetAt(0, 1, 6);
            tilemap.SetAt(1, 1, 8);
            tilemap.SetAt(0, 2, 6);
            tilemap.SetAt(1, 2, 8);
            tilemap.SetAt(0, 3, 6);
            tilemap.SetAt(1, 3, 8);

            tilemap.SetAt(0, 4, 6);
            tilemap.SetAt(1, 4, 7);
            tilemap.SetAt(2, 4, 13);

            tilemap.SetAt(0, 5, 6);
            tilemap.SetAt(1, 5, 7);
            tilemap.SetAt(2, 5, 17);

            tilemap.SetAt(3, 5, 4);
            tilemap.SetAt(4, 5, 4);
            tilemap.SetAt(5, 5, 4);
            tilemap.SetAt(6, 5, 4);
            tilemap.SetAt(7, 5, 4);


            GameObjects =
            [
                player,
                mainMenuButton,
                ..tilemap.GameObjects.Values
            ];

            Camera = new Camera(player);
        }

        public override void Update(HashSet<Keyboard.Key> pressedKeys)
        {
            base.Update(pressedKeys);

            float speed = 500f;
            float jumpForce = 750f;

            player.Velocity = new Vector2(0, player.Velocity.Y);

            if (pressedKeys.Contains(Keyboard.Key.A))
                player.Velocity = new Vector2(-speed, player.Velocity.Y);
            if (pressedKeys.Contains(Keyboard.Key.D))
                player.Velocity = new Vector2(speed, player.Velocity.Y);
            if (pressedKeys.Contains(Keyboard.Key.Space) && player.IsGrounded)
                player.Velocity += new Vector2(0, -jumpForce);
            if (pressedKeys.Contains(Keyboard.Key.R))
                SceneManager.LoadScene(SceneManager.CurrentSceneId);

            //player.Velocity = new Vector2(Math.Clamp(player.Velocity.X, -speed * 1000, speed * 1000), player.Velocity.Y);
        }

        public override void Draw(RenderWindow window, GameObject gameObject)
        {
            Camera.Draw(window, gameObject);
        }
    }
}
