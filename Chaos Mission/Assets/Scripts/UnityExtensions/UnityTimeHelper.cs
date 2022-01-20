using System;
using UnityEngine;

namespace ChaosMission.UnityExtensions
{
    public static class UnityTimeHelper
    {
        public static int GetMillisecondsToNextFixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            float timeFromLastFixedUpdate = (Time.realtimeSinceStartup - Time.fixedUnscaledTime) % fixedDeltaTime;
            double inSeconds = Math.Round(fixedDeltaTime - timeFromLastFixedUpdate, 3);
            return (int)(inSeconds * 1000);
        }
    }
}
