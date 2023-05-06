using Events;
using Util;

namespace Combat.HitPoints
{
    public interface IHealthPotion
    {
        void Use();
        SpellUsed GetSpellUsedEvent();
        ICooldownManager GetCooldownManager();
    }
}