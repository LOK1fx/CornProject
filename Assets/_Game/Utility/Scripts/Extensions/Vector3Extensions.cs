using UnityEngine;

namespace LOK1game.Tools
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Vector3 Multiply method
        /// </summary>
        /// <param name="a">First multiplyer</param>
        /// <param name="b">Second multiplyer</param>
        /// <returns>Product</returns>
        public static Vector3 MultiplyBy(this Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 GetDirectionTo(this Vector3 a, Vector3 b)
        {
            return (a - b).normalized;
        }
    }
}