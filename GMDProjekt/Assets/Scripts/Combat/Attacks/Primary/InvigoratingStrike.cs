using DefaultNamespace;
using Items;
using UnityEngine;
using UnityEngine.ProBuilder;
using Math = System.Math;

namespace Combat.Attacks
{
    public class InvigoratingStrike : MonoBehaviour, IPrimaryAttack
    {
        public int BaseDamage = 20;
        public string AttackAudioClipName;
        public int Range = 2;
        public int ResourceGeneratedPerAttack = 15;
        private Animator _animator;
        private Inventory _inventory;
        private void Start()
        {
            _inventory = GetComponent<Inventory>();
            _animator = GetComponent<Animator>();
        }

        public bool Attack(IAttackable toAttack)
        {
            if (!AttackUtil.IsInRageToAttack(Range, transform.position, toAttack.GetPosition()))
            {
                return false;
            }
            _animator.Play("InvigoratingStrike");
            var damage = AttackUtil.GetDamageAfterMultiplier(_inventory.GetAmountOfMainStat(), BaseDamage);
            toAttack.Attack(damage);
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

