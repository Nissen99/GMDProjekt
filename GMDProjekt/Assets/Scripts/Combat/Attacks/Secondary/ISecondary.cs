using Combat.Attacks;

namespace Spells
{
    public interface ISecondary : IAttack
    {
        int GetCostOfAttack();
    }
}