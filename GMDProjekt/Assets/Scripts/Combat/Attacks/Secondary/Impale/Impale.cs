using System;
using Combat.Attacks;
using DefaultNamespace;
using Items;
using UnityEngine;

namespace Spells.Impale
{
    public class Impale : MonoBehaviour, ISecondary
    {
        public int Range = 40;

        public int BaseDamge = 40;
        public float TimeBeforeKnifeDespawns = (float)2.5;
        public float SpeedOfKnifes = 15;
        public int AngleOfKnifes = 10;
        public int CostOfAttack = 15;
        public ImpaleKnife ImpaleKnife;
        private Inventory _inventory;

        private Animator _animator;

        private void Start()
        {
            _inventory = GetComponent<Inventory>();
            _animator = GetComponent<Animator>();
        }

        public bool Attack(IAttackable toAttack)
        {
            _animator.Play("Impale");
            var toAttackPosition = toAttack.GetPosition();
            if (AttackUtil.IsInRageToAttack(Range, transform.position, toAttackPosition))
            {
                FindObjectOfType<AudioManager>().Play(GetType().Name);
                Vector3 direction = toAttackPosition - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                createKnife(rotation);

                if (_inventory.HasItemWithNameEquipped(Legendary_Item_Names.HOLY_POINT_SHOT))
                {
                    Quaternion leftRotation = Quaternion.AngleAxis(-AngleOfKnifes, Vector3.up) * rotation;
                    Quaternion rightRotation = Quaternion.AngleAxis(AngleOfKnifes, Vector3.up) * rotation;
                    createKnife(leftRotation);
                    createKnife(rightRotation);
                }

                return true;
            }

            return false;
        }

        public int GetCostOfAttack()
        {
            return CostOfAttack;
        }

        public int GetRange()
        {
            return Range;
        }

        //Setting the Damage and Speed of knifes on Impale rather than the prefab, as that is what change the gameplay
        private void createKnife(Quaternion rotation)
        {
            var knife = Instantiate(ImpaleKnife,
                new Vector3(transform.position.x, (float)0.5, transform.position.z), rotation);
            var damage = BaseDamge;
            if (_inventory.HasItemWithNameEquipped(Legendary_Item_Names.KARLEIS_POINT))
            {
                damage = (int)(damage * 3.75);
            }

            damage = AttackUtil.GetDamageAfterMultiplier(_inventory.GetAmountOfMainStat(), damage);
            Debug.Log($"Damage per knife: {damage}"); //Keeping an eye on it as there are alot of moving parts to make the damage 
            knife.SetAttack(damage);
            knife.SetSpeed(SpeedOfKnifes);
            Destroy(knife.gameObject, TimeBeforeKnifeDespawns);
        }
    }
}