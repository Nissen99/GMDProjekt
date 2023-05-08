using DefaultNamespace;
using Events;
using JetBrains.Annotations;
using UnityEngine;
using Util;

namespace Spells
{
    public interface ISpell
    {
        bool Cast(Vector3? positionToCast, [CanBeNull] IAttackable toAttack = null);

        bool IsReady();
        ICooldownManager GetCooldownManager();
        SpellUsed GetSpellUsedEvent();

    }
}