using AGWAE.Scenes;

namespace AGWAE
{
    internal static class SceneManager
    {
        private static readonly List<Func<Scene>> scenes =
        [
            () => new SMainMenu(),
            () => new SGameScene()
        ];

        public static Scene CurrentScene { get; private set; }
        public static int CurrentSceneId { get; private set; }

        static SceneManager()
        {
            CurrentSceneId = 0;
            CurrentScene = scenes[CurrentSceneId]();
            LoadScene(CurrentSceneId);
        }

        public static void LoadScene(int sceneId)
        {
            if (sceneId < 0 || sceneId >= scenes.Count)
                throw new ArgumentOutOfRangeException(nameof(sceneId), "Invalid scene ID");

            CurrentSceneId = sceneId;

            CurrentScene = scenes[sceneId]();
            CurrentScene.Start();
        }
    }
}
