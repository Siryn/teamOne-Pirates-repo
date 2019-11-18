using UnityEngine;

namespace Extensions
{

    public static class TransformExtensions
    {
        /// <summary>
        /// Check if the target is within line of sight
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="target"></param>
        /// <param name="fieldOfView"></param>
        /// <param name="collisionMask"></param>
        /// <param name="offset"></param>
        /// <returns></returns>

        public static bool IsInLineOfSight(this Transform origin, Vector3 target, float fieldOfView, LayerMask collisionMask, Vector3 offset)
        {
            Vector3 direction = target - origin.position;

            if (Vector3.Angle(origin.forward, direction.normalized) < fieldOfView / 2)
            {
                float distanceToTarget = Vector3.Distance(origin.position, target);
                RaycastHit hit;
                if (Physics.Raycast(origin.position + offset + origin.forward * .3f, direction.normalized, out hit, distanceToTarget, collisionMask))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }

}
