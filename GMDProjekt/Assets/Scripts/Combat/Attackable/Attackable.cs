using UnityEngine;

namespace DefaultNamespace
{
    public class Attackable: MonoBehaviour, IAttackable
    {
        private IHpController _hpController;
        
        void Start()
        {
            _hpController = GetComponent<IHpController>();
        }
        public void Attack(int damage)
        {
            _hpController.Damage(damage);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool IsAlive()
        {
            return transform != null;
        }
    }
}