namespace DefaultNamespace
{
    public interface IHpController
    {
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        void Damage(int amount);
    }
}