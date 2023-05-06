using DefaultNamespace;

namespace Combat.Attacks
{
    public interface IAttack
    { int GetRange();
        /*
         * Will return weather or not the attack started.
         */
        bool Attack(IAttackable toAttack);
    }
}