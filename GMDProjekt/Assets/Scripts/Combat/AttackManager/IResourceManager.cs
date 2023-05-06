namespace Combat.AttackManager
{
    public interface IResourceManager
    {
        bool HasEnough(int amount);
        void Spend(int amount);
        void Generate(int amount);
    }
}