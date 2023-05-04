using UnityEngine;

namespace DefaultNamespace
{
    public interface IAttackable
    {
        void Attack(int damage);
        Vector3 GetPosition();
        bool IsAlive();
    }
}