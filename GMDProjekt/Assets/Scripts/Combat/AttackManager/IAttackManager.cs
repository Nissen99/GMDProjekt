using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat.AttackManager
{
    public interface IAttackManager
    {
        void PrimaryAttack(IAttackable toAttack);
        void StopPrimaryAttackIntent();
        void SecondaryAttack(IAttackable toAttack);
    }
}