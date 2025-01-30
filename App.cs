namespace AGWAE
{
    internal static class App
    {
        public static Action? Closed;
        public static Action? FixedUpdateCalled;

        public static void Quit()
        {
            Closed?.Invoke();
        }

        public static void FixedUpdate()
        {
            FixedUpdateCalled?.Invoke();
        }
    }
}
