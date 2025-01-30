using AGWAE;
using SFML.Graphics;
using SFML.Window;

class Program
{
    private static readonly HashSet<Keyboard.Key> pressedKeys = [];

    private static void Main()
    {
        var window = new RenderWindow(new VideoMode(Config.WINDOW_SIZE_X, Config.WINDOW_SIZE_Y), Config.WINDOW_TITLE);

        window.Closed += (sender, e) => window.Close();
        App.Closed += window.Close;
        App.FixedUpdateCalled += () => SceneManager.CurrentScene.FixedUpdate(pressedKeys);

        window.KeyPressed += (sender, e) => pressedKeys.Add(e.Code);
        window.KeyReleased += (sender, e) => pressedKeys.Remove(e.Code);
        window.MouseButtonPressed += (sender, e) => SceneManager.CurrentScene.HandleClick(e);

        Time.Update();

        while (window.IsOpen)
        {
            Time.Update();

            window.DispatchEvents();
            window.Clear(Color.Black);

            SceneManager.CurrentScene.Update(pressedKeys);
            SceneManager.CurrentScene.Draw(window);

            window.Display();
        }

    }
}
