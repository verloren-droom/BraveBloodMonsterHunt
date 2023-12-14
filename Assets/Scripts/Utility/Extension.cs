using System.Linq;

namespace BraveBloodMonsterHunt.Utility
{
    public static class Extension
    {
        /// <summary>
        /// Extension: System.Random
        /// </summary>
        /// <param name="random"></param>
        /// <param name="minValue">min value</param>
        /// <param name="maxValue">max value</param>
        /// <param name="floatPlace"></param>
        /// <returns>random float value</returns>
        public static float NextFloat(this System.Random random, float minValue = 0.0f, float maxValue = 1.0f, int floatPlace = 2)
        {
            return System.Convert.ToSingle((System.Convert.ToSingle(random.NextDouble()) * (maxValue - minValue) +
                                            minValue).ToString($"F{floatPlace}"));
        }

        /// <summary>
        /// determines whether the value in the array is not empty and the array length is greater than zero
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>true -> the array is not empty and the array length is greater than zero, false ->  </returns>
        public static bool IsNotNullAndLengthGreaterZero<T>(this T[] array)
        {
            return array.Length > 0 && array.Any(obj => obj != null);
        }
    }
}