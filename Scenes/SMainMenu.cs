using SFML.Graphics;
using System.Numerics;

namespace AGWAE.Scenes
{
    internal class SMainMenu : GameScene
    {
        protected Camera Camera { get; }
        protected override List<GameObject> GameObjects { get; }
        public override string Name { get; protected set; } = "MainMenu";

        private readonly UIObject startButton = new(new(0, 0), Quaternion.Zero, new(5, 5), Layer.UI, new(Sprites.Play));
        private readonly UIObject quitButton = new(new(0, 0), Quaternion.Zero, new(5, 5), Layer.UI, new(Sprites.Quit));

        public SMainMenu()
        {
            startButton.Position = new(Sprites.GetCenterXOf(startButton), Sprites.GetCenterYOf(startButton) - 50);
            quitButton.Position = new(Sprites.GetCenterXOf(quitButton), Sprites.GetCenterYOf(quitButton) + 50);

            startButton.Clicked += () => SceneManager.LoadScene(1);
            quitButton.Clicked += App.Quit;

            GameObjects = [startButton, quitButton];
            Camera = new Camera(new GameObject(new(0, 0), Layer.Ground, new Sprite()));
        }

        public override void Draw(RenderWindow window, GameObject gameObject)
        {
            Camera.Draw(window, gameObject);
        }
    }
}
