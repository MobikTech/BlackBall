using UnityEngine;

namespace BlackBall.Common.Math
{
    public static class MathAddition
    {
        public static bool AreEqual(float value1, float value2, float approximation) =>
            System.Math.Abs(value1 - value2) < approximation;

        public static bool AreEqual(Vector3 value1, Vector3 value2, float approximation) =>
            System.Math.Abs(value1.x - value2.x) < approximation &&
            System.Math.Abs(value1.y - value2.y) < approximation &&
            System.Math.Abs(value1.z - value2.z) < approximation;

        public static Vector3 CalculateQuadraticBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            return (uu * p0) + (2 * u * t * p1) + (tt * p2);
        }

        public static Vector3 CalculateCubicBezier(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;
        
            return (uuu * p0) + (3 * uu * t * p1) + (3 * u * tt * p2) + ttt * p3;
        }
        
        public static float Absolute(float value) => value * GetSign(value);
        public static bool LargerThanZero(float value) => value > 0.0f;
        public static bool LessThanZero(float value) => value < 0.0f;
        public static bool EqualsZero(float value) => value == 0.0f;
        public static bool IsSameSign(float value1, float value2) =>
            LargerThanZero(value1) && LargerThanZero(value2)
            || LessThanZero(value1) && LessThanZero(value2);

        public static float GetSign(float value)
        {
            float result;
            
            if (LargerThanZero(value)) result = 1f;
            else if(LessThanZero(value)) result = -1f;
            else result = 0f;
        
            return result;
        }
        
        // assumed that ZERO has no sign. If here is zero value returned false
        public static bool HaveDifferentSigns(float value1, float value2) =>
            !IsSameSign(value1, value2) 
            && !EqualsZero(value1)
            && !EqualsZero(value2);



    }
}