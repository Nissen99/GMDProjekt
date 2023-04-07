using System;
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
            if (canAttack(_primaryAttack.AttackRange, positionOfToAttack))
            {
                _toPrimaryAttack = null;
                _movement.StopMoving();
                _primaryAttack.Attack(toAttack);
            }
            else
            {
                _movement.Move(positionOfToAttack);
                _toPrimaryAttack = toAttack;
            }
        }

        public void StopPrimaryAttackIntent()
        {
            _toPrimaryAttack = null;
        }


        private bool canAttack(int attackRange, Vector3 toAttackPosition)
        {
            var distanceToAttack = Vector3.Distance(transform.position, toAttackPosition);
            return distanceToAttack <= attackRange;
        }
    }
}