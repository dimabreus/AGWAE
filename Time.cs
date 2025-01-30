using System.Diagnostics;

namespace AGWAE
{
    internal static class Time
    {
        public static float DeltaTime { get; private set; }
        //public const float FixedDeltaTime = 1f / 50f;

        private static float lastTime = 0f;
        //private static float accumulatedTime = 0f;

        public static void Update()
        {
            float currentTime = Stopwatch.GetTimestamp() / (float)Stopwatch.Frequency;
            DeltaTime = currentTime - lastTime;
            lastTime = currentTime;

            //accumulatedTime += DeltaTime;
            //while (accumulatedTime >= FixedDeltaTime)
            //{
            //    App.FixedUpdate();
            //    accumulatedTime -= FixedDeltaTime;
            //}
        }
    }

}
