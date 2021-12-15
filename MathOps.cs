using UnityEngine;
namespace DomsUnityHelper
{
    /// <summary>
    /// A class for common mathematical needs
    /// </summary>
    public static class MathOps
    {
        #region Averages
        public static float Average(params float[] numsToAverage)
        {
            float sum = 0f;
            foreach(float f in numsToAverage)
            {
                sum += f;
            }

            return sum / numsToAverage.Length;
        }

        public static double Average(params double[] numsToAverage)
        {
            double sum = 0f;
            foreach(double f in numsToAverage)
            {
                sum += f;
            }

            return sum / numsToAverage.Length;
        }

        public static float Average(params int[] numsToAverage)
        {
            float sum = 0f;
            foreach(float f in numsToAverage)
            {
                sum += f;
            }
            return sum / numsToAverage.Length;
        }

        public static Vector2 Average(params Vector2[] vecsToAverage)
        {
            return vecsToAverage.Average();
        }

        public static Vector3 Average(params Vector3[] vecsToAverage)
        {
            return vecsToAverage.Average();
        }
        public static Vector4 Average(params Vector4[] vecsToAverage)
        {
            return vecsToAverage.Average();
        }
        #endregion Averages

        #region Mapping
        public static Vector2 Map(Vector2 valueToMap, Vector2 in_min, Vector2 in_max, Vector2 out_min, Vector2 out_max)
        {
            return valueToMap.Map(in_min, in_max, out_min, out_max);
        }

        public static Vector3 Map(Vector3 valueToMap, Vector3 in_min, Vector3 in_max, Vector3 out_min, Vector3 out_max)
        {
            return valueToMap.Map(in_min, in_max, out_min, out_max);
        }

        public static Vector4 Map(Vector4 valueToMap, Vector4 in_min, Vector4 in_max, Vector4 out_min, Vector4 out_max)
        {
            return valueToMap.Map(in_min, in_max, out_min, out_max);
        }

        public static double Map(double valueToMap, double in_min, double in_max, double out_min, double out_max)
        {
            return valueToMap.Map(in_min, in_max, out_min, out_max);
        }

        public static float Map(float valueToMap, float in_min, float in_max, float out_min, float out_max)
        {
            return valueToMap.Map(in_min, in_max, out_min, out_max);
        }

        public static int Map(int valueToMap, int in_min, int in_max, int out_min, int out_max)
        {
            return valueToMap.Map(in_min, in_max, out_min, out_max);
        }
        #endregion Mapping
    }
}