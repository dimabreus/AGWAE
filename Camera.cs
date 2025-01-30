using SFML.Graphics;
using System.Numerics;

namespace AGWAE
{
    internal class Camera(GameObject followObject)
    {
        private readonly GameObject followObject = followObject;

        public void Draw(RenderWindow window, GameObject gameObject)
        {
            if (gameObject is UIObject)
            {
                gameObject.Draw(window);
                return;
            }

            var width = followObject.Sprite.TextureRect.Width;
            var height = followObject.Sprite.TextureRect.Height;

            gameObject.Draw(
                window,
                gameObject.Position
                - followObject.Position
                + new Vector2(Config.WINDOW_SIZE_X, Config.WINDOW_SIZE_Y) / 2
                - new Vector2(width * followObject.Scale.X, height * followObject.Scale.Y) / 2
                );
        }
    }
}
