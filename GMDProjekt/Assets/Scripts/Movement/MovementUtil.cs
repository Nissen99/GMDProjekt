using UnityEngine;

namespace DefaultNamespace
{
    public class MovementUtil
    {
        private static float _acceptedErrorMargin = 0.1f;
        public static readonly int animatorSpeed = Animator.StringToHash("Speed");

        public static bool CheckEqualWithinErrorMargin(Vector3 v1, Vector3 v2)
        {
            if (Mathf.Abs(v1.x - v2.x) > _acceptedErrorMargin)
            {
                return false;
            }

            if (Mathf.Abs(v1.y - v2.y) > _acceptedErrorMargin)
            {
                return false;
            }

            if (Mathf.Abs(v1.z - v2.z) > _acceptedErrorMargin)
            {
                return false;
            }

            return true;
        }
    }
}