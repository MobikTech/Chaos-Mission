using UnityEngine;

namespace ChaosMission.Extensions.UnityExtensions
{
    public static class UnityTimeHelper
    {
        private const int MillisecondsInSeconds = 1000;
        private const int RoundAccuracy = 3;
        private static readonly float s_fixedDeltaTime = Time.fixedDeltaTime;


        public static int GetMillisecondsToNextFixedUpdate()
        {
            float timeFromLastFixedUpdate = (Time.realtimeSinceStartup - Time.fixedUnscaledTime) % s_fixedDeltaTime;
            float inSeconds = s_fixedDeltaTime - timeFromLastFixedUpdate;
            return (int)(inSeconds * MillisecondsInSeconds);
        }
    }
}
