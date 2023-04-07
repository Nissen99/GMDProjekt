namespace DefaultNamespace
{
    public interface IAttack
    {
        public int AttackRange { get; }
        void Attack(IAttackable toAttack);
    }
}