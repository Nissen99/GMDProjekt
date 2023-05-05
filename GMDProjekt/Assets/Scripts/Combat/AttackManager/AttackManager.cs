using System;
using Combat.Attacks;
using DefaultNamespace;
using Spells;
using TMPro;
using UnityEngine;

namespace Combat.AttackManager
{
    public class AttackManager : MonoBehaviour, IAttackManager
    {


        private IAttackable _toPrimaryAttack;
        
        private bool _isAttacking;


        private IMovement _movement;
        private IPrimaryAttack _primaryPrimaryAttack;
        private ISecondary _secondaryAttack;
        
        [SerializeField] private float _globalCooldown = 2f;

        private float? nextTimeToAttack;
        void Start()
        {
            _primaryPrimaryAttack = GetComponent<IPrimaryAttack>();
            _secondaryAttack = GetComponent<ISecondary>();
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
            var performedAttack = false;
            if (!isOnCooldown())
            {
                performedAttack = _primaryPrimaryAttack.Attack(toAttack);
            }
            if (performedAttack)
            {
                attacked(toAttack.GetPosition());
            }
            else
            {
                moveIfNotInRange(_primaryPrimaryAttack.Range, toAttack.GetPosition());
                _toPrimaryAttack = toAttack;
            }
        }
        

        public void StopPrimaryAttackIntent()
        { 
            _toPrimaryAttack = null;
        }

        public void SecondaryAttack(IAttackable toAttack )
        {
            if (!isOnCooldown())
            {
                var performedAttack = _secondaryAttack.Attack(toAttack);
                if (performedAttack)
                {
                    attacked(toAttack.GetPosition());
                }
            }
        }


        void attacked(Vector3 positionOfAttacked)
        {
            _toPrimaryAttack = null;
            _movement.StopMoving();
            transform.LookAt(positionOfAttacked);
            nextTimeToAttack = Time.time + _globalCooldown; 
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