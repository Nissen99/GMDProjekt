using DefaultNamespace;

namespace Combat.AttackManager
{
    public interface IAttackManager
    {
        void PrimaryAttack(IAttackable toAttack);
        void StopPrimaryAttackIntent();
    }
}