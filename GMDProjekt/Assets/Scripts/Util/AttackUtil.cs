
using System;
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
        
        public static int GetDamageAfterMultiplier(int amountOfMainStat, int BaseDamage)
        {
            var mainStatMultiplier = Math.Pow(amountOfMainStat, 0.5) + 1;
            return (int)(BaseDamage * mainStatMultiplier);
        }
    }
}