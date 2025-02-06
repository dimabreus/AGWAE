using AGWAE;
using SFML.Graphics;
using SFML.Window;

class Program
{
    private static readonly HashSet<Keyboard.Key> pressedKeys = [];
    private static RenderWindow? window;

    private static void Main()
    {
        window = new RenderWindow(new VideoMode(Config.WINDOW_SIZE_X, Config.WINDOW_SIZE_Y), Config.WINDOW_TITLE);

        window.Closed += (sender, e) => window.Close();
        App.Closed += window.Close;
        App.FixedUpdateCalled += () => SceneManager.CurrentScene.FixedUpdate(pressedKeys);

        window.KeyPressed += (sender, e) => pressedKeys.Add(e.Code);
        window.KeyReleased += (sender, e) => pressedKeys.Remove(e.Code);
        window.MouseButtonPressed += (sender, e) => SceneManager.CurrentScene.HandleClick(e);
        window.MouseButtonReleased += (sender, e) => SceneManager.CurrentScene.HandleClickRelease(e);
        window.MouseMoved += (sender, e) => SceneManager.CurrentScene.HandleMouseMoved(e);
        window.MouseWheelScrolled += (sender, e) => SceneManager.CurrentScene.HandleMouseWheelScrolled(e);
        window.Resized += (sender, e) => HandleWindowResize(e);

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

    private static void HandleWindowResize(SizeEventArgs e)
    {
        window?.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
    }
}
