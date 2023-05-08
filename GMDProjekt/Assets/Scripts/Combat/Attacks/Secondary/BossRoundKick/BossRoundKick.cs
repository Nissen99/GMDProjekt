using System.Collections;
using System.Collections.Generic;
using Combat.Attacks;
using Combat.Attacks.Secondary.BossRoundKick;
using DefaultNamespace;
using Spells;
using UnityEngine;
using Util;

public class BossRoundKick : MonoBehaviour, ISecondary
{

        public int Range;
        public int CostOfAttack;
        public int BaseDamage = 100;
        public string AttackClipName;
        public bool ShouldScaleWithDifficulty;
        public float DelayBeforeExploding;
        
        private Animator _animator;
        private int _damage;

        public ExplosionZone ExplosionZonePrefab;
        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            if (ShouldScaleWithDifficulty)
            {
                scaleWithDifficulty();
            }
            else
            {
                _damage = BaseDamage;
            } 
        }
    
        public int GetRange()
        {
            return Range;
        }

        public bool Attack(IAttackable toAttack)
        {
            if (!AttackUtil.IsInRageToAttack(Range, transform.position, toAttack.GetPosition()))
            {
                return false;
            }
           // _animator.Play("BossRoundKick");
            FindObjectOfType<AudioManager>().Play(AttackClipName);

            // Spawn in the cicle that explodes for damage
            var explosionZone = Instantiate(ExplosionZonePrefab, transform.position, Quaternion.identity);
            explosionZone.SetDamage(_damage);
            Destroy(explosionZone.gameObject, DelayBeforeExploding);
            return true;
        }

        public int GetCostOfAttack()
        {
            return CostOfAttack;
        }
    
        private void scaleWithDifficulty()
        {
            var difficultyMultiplier = DifficultyManager.GetInstance().GetDifficultyMultiplier();
            _damage = difficultyMultiplier * BaseDamage;
        }

}
