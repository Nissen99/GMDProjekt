using DefaultNamespace;
using UnityEngine;

namespace Combat.Attacks
{
    public class InvigoratingStrike : MonoBehaviour, IAttack
    {
        public int BaseDamage { get; } = 20;
        public int Range { get; } = 2;
        public float Cooldown { get; } = 0f;

        private Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public bool Attack(IAttackable toAttack)
        {
            _animator.Play("InvigoratingStrike");
            return toAttack.Attack(BaseDamage);
        }
    
    }
}

