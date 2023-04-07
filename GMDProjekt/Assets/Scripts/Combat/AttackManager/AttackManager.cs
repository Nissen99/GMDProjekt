using System;
using DefaultNamespace;
using UnityEngine;

namespace Combat.AttackManager
{
    public class AttackManager : MonoBehaviour, IAttackManager
    {


        private IAttackable _toPrimaryAttack;
        
        private bool _isAttacking;


        private IPlayerMovement _playerMovement;
        private IAttack _primaryAttack;
        void Start()
        {
            _primaryAttack = GetComponent<InvigoratingStrike>();
            _playerMovement = GetComponent<IPlayerMovement>();
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
                _playerMovement.StopMoving();
                _playerMovement.FacePoint(positionOfToAttack);
                _primaryAttack.Attack(toAttack);
            }
            else
            {
                _playerMovement.Move(positionOfToAttack);
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