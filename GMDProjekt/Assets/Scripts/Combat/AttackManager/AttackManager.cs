using System;
using Combat.Attacks;
using DefaultNamespace;
using UnityEngine;

namespace Combat.AttackManager
{
    public class AttackManager : MonoBehaviour, IAttackManager
    {


        private IAttackable _toPrimaryAttack;
        
        private bool _isAttacking;


        private IMovement _movement;
        private IAttack _primaryAttack;
        
        [SerializeField] private float _globalCooldown = 2f;

        private float? nextTimeToAttack;
        void Start()
        {
            _primaryAttack = GetComponent<InvigoratingStrike>();
            _movement = GetComponent<IMovement>();
        }

        private void FixedUpdate()
        {
            if (!_isAttacking && _toPrimaryAttack != null)
            {
                PrimaryAttack(_toPrimaryAttack);
            }
            
        }

        public void PrimaryAttack(IAttackable toAttack)
        {
            var positionOfToAttack = toAttack.GetPosition();
            if (canAttack(_primaryAttack.Range, positionOfToAttack))
            {
                _toPrimaryAttack = null;
                _movement.StopMoving();
               var died = _primaryAttack.Attack(toAttack);
               nextTimeToAttack = Time.time + _globalCooldown;
               if (died)
               {
                   _toPrimaryAttack = null;
               }
            }
            else
            {
                moveIfNotInRange(_primaryAttack.Range, positionOfToAttack);
                _toPrimaryAttack = toAttack;
            }
        }

        public void StopPrimaryAttackIntent()
        {
            _toPrimaryAttack = null;
        }

        bool canAttack(int attackRange, Vector3 toAttackPosition)
        {
            return !isOnCooldown() && isInRangeToAttack(attackRange, toAttackPosition);
            
        }
        

        bool isInRangeToAttack(int attackRange, Vector3 toAttackPosition)
        {
            var distanceToAttack = Vector3.Distance(transform.position, toAttackPosition);
            return distanceToAttack <= attackRange;
        }

        bool isOnCooldown()
        {
            if (nextTimeToAttack == null)
            {
                return false;
            }

            return nextTimeToAttack > Time.time;
        }

        void moveIfNotInRange(int attackRange, Vector3 toAttackPosition)
        {
            if (isInRangeToAttack(attackRange, toAttackPosition))
            {
                return;
            }
            
            _movement.Move(toAttackPosition);
        }
    }
}