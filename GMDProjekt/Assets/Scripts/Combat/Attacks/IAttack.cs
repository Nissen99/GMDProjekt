using DefaultNamespace;

namespace Combat.Attacks
{
    public interface IAttack
    {
        public int Range { get; }
        /*
         * Will return weather or not the attack started.
         */
        bool Attack(IAttackable toAttack);
    }
}