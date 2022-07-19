namespace ChaosMission.Common
{
    public static class CustomMath
    {
        public static bool IsSameSign(float value1, float value2) =>
            LargerThanZero(value1) && LargerThanZero(value2)
            || LessThanZero(value1) && LessThanZero(value2);

        public static bool LargerThanZero(float value) => value > 0.0f;
        public static bool LessThanZero(float value) => value < 0.0f;
        public static bool EqualsZero(float value) => value == 0.0f;

        public static float GetSign(float value)
        {
            float result;
            
            if (LargerThanZero(value)) result = 1f;
            else if(LessThanZero(value)) result = -1f;
            else result = 0f;

            return result;
        }
        
        // assumed that ZERO has no sign. If here is zero value - returned false
        public static bool HaveDifferentSigns(float value1, float value2) =>
            !IsSameSign(value1, value2) 
            && !EqualsZero(value1)
            && !EqualsZero(value2);

        public static float Absolute(float value) => value * GetSign(value);

        public static bool Approximate(float a, float b, float tolerance) => (Absolute(a - b) < tolerance);
    }
}