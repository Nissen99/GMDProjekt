using DefaultNamespace;
using UnityEngine;

namespace Combat.Attacks
{
    public class InvigoratingStrike : MonoBehaviour, IPrimaryAttack
    {
        public int BaseDamage = 20;
        public string AttackAudioClipName;
        public int Range = 2;
        public int ResourceGeneratedPerAttack = 15;
        private Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public bool Attack(IAttackable toAttack)
        {
            if (!AttackUtil.IsInRageToAttack(Range, transform.position, toAttack.GetPosition()))
            {
                return false;
            }
            _animator.Play("InvigoratingStrike");
            toAttack.Attack(BaseDamage);
            FindObjectOfType<AudioManager>().Play(AttackAudioClipName);
            return true;
        }

        public int GetRange()
        {
            return Range;
        }

        public int GetResourceGeneratedPerAttack()
        {
            return ResourceGeneratedPerAttack;
        }


    }
}

