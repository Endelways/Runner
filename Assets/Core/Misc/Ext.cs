using UnityEngine;

namespace Core.Misc
{
    public static class Ext
    {
        public static Vector3 Copy(this Vector3 vec)
        {
            var newVec = new Vector3() { x = vec.x, y = vec.y, z = vec.z };
            return newVec;
        }
    }
}