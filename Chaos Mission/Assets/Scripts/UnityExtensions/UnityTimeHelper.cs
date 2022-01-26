using System;
using UnityEngine;

namespace ChaosMission.UnityExtensions
{
    public static class UnityTimeHelper
    {
        private const int MillisecondsInSeconds = 1000;
        private const int RoundAccuracy = 3;
        private static readonly float FixedDeltaTime = Time.fixedDeltaTime;


        public static int GetMillisecondsToNextFixedUpdate()
        {
            float timeFromLastFixedUpdate = (Time.realtimeSinceStartup - Time.fixedUnscaledTime) % FixedDeltaTime;
            float inSeconds = FixedDeltaTime - timeFromLastFixedUpdate;
            return (int)(inSeconds * MillisecondsInSeconds);
        }
    }
}
