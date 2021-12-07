
namespace DomsUnityHelper
{
    /// <summary>
    /// A class for defining a range of values with a min, max, and even a default value. Nicer on the eyes and brain than a Vector2
    /// </summary>
    /// <typeparam name="T">Type of range you'd like to make - takes any type</typeparam>
    [System.Serializable]
    public class Range<T>
    {
        public T min;
        public T max;
        public readonly T defaultValue;

        public Range(T min, T max, T defaultValue = default(T))
        {
            this.min = min;
            this.max = max;
            this.defaultValue = defaultValue;
        }
    }
}