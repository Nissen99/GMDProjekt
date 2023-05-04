
using UnityEngine;

namespace Combat.Attacks
{
    public class AttackUtil
    {
        public static bool IsInRageToAttack(int attackRange, Vector3 currentPosition, Vector3 tragetsPosition)
        {
            var distanceToAttack = Vector3.Distance(currentPosition, tragetsPosition);
            return distanceToAttack <= attackRange;
        }
    }
}