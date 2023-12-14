using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BraveBloodMonsterHunt.Utility
{
    /// <summary>
    /// random add other function utility
    /// </summary>
    public static class RandomUtility
    {
        /// <summary>
        /// inside rectangle
        /// </summary>
        /// <param name="center">center pointer</param>
        /// <param name="size">size pointer</param>
        /// <returns>random vector2 value</returns>
        public static Vector2 InsideRectangle(Vector2 center, Vector2 size)
        {
            Vector2 topLeft = center - size / 2;
            // Vector2 topRight = center + new Vector2(size.x / 2, -size.y / 2);
            // Vector2 bottomLeft = center - new Vector2(size.x / 2, size.y / 2);
            Vector2 bottomRight = center + size / 2;

            float randomX = Random.Range(topLeft.x, bottomRight.x);
            float randomY = Random.Range(topLeft.y, bottomRight.y);

            return new Vector2(randomX, randomY);
        }

        public static float RangeVector2(Vector2 minMaxValue)
        {
            return UnityEngine.Random.Range(minMaxValue.x, minMaxValue.y);
        }
        public static int RangeVector2(Vector2Int minMaxValue)
        {
            return UnityEngine.Random.Range(minMaxValue.x, minMaxValue.y);
        }
        
        public static int RandomArrayIndex(IList<float> array)
        {
            var totalProbability = array.Sum();
            float ranValue = UnityEngine.Random.Range(0.0f, totalProbability);
            return RandomArrayIndex(array, ranValue);
        }

        public static int RandomArrayIndex(IList<float> array, int seed)
        {
            var totalProbability = array.Sum();
            var rand = new System.Random(seed);
            return RandomArrayIndex(array, rand.NextFloat(0.0f, totalProbability));
        }

        /// <summary>
        /// random Array/List index
        /// </summary>
        /// <param name="array"></param>
        /// <param name="ranValue"></param>
        /// <returns>array/list index</returns>
        private static int RandomArrayIndex(IList<float> array, float ranValue)
        {
            if (array.Count == 0)
            {
                Debug.LogError("array count less one!");
                return -1;
            }
            
            float cumulativeProbability = 0f;
            for (var i = 0; i < array.Count; i++)
            {
                cumulativeProbability += array[i];
                if (ranValue <= cumulativeProbability)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}