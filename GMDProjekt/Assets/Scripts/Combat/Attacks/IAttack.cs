using Palmmedia.ReportGenerator.Core.Parser;

namespace DefaultNamespace
{
    public interface IAttack
    {
        public int BaseDamage { get; }
        public int Range { get; }
        public float Cooldown { get;  }
        bool Attack(IAttackable toAttack);
        
    }
}