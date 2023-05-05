using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;

namespace Spells
{
    public interface ISpell
    {
        public int Range { get; }
        bool Cast(Vector3? positionToCast, [CanBeNull] IAttackable toAttack = null);
    }
}