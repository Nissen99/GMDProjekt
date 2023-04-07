using UnityEngine;

namespace DefaultNamespace
{
    public interface IAttackable
    {
        bool Attack(int damage);
        Vector3 GetPosition();
        bool IsAlive();
    }
}