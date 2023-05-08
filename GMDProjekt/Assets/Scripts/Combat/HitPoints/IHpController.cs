namespace DefaultNamespace
{
    public interface IHpController
    {
        void Damage(int amount);
        void Heal(int amount);

        int GetMaxHealth();
        int GetCurrentHealth();
        bool IsAlive();
        void MakeImmune();
        void MakeDamageable();
    }
}