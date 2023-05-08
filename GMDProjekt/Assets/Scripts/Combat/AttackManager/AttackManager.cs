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

        private IResourceManager _resourceManager;
        private IMovement _movement;
        private IPrimaryAttack _primaryPrimaryAttack;
        private ISecondary _secondaryAttack;
        
        [SerializeField] private float _globalCooldown = 1f;

        private float? nextTimeToAttack;
        void Start()
        {
            _primaryPrimaryAttack = GetComponent<IPrimaryAttack>();
            _secondaryAttack = GetComponent<ISecondary>();
            _movement = GetComponent<IMovement>();
            _resourceManager = GetComponent<IResourceManager>();
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
            if (canAttack(toAttack.IsAlive()))
            {
                performedAttack = _primaryPrimaryAttack.Attack(toAttack);
            }
            if (performedAttack)
            {
                attacked(toAttack.GetPosition(), _primaryPrimaryAttack.GetResourceGeneratedPerAttack(), false);
            }
            else
            {
                moveIfNotInRange(_primaryPrimaryAttack.GetRange(), toAttack.GetPosition());
                _toPrimaryAttack = toAttack;
            }
        }
        

        public void StopPrimaryAttackIntent()
        { 
            _toPrimaryAttack = null;
        }

        public void SecondaryAttack(IAttackable toAttack )
        {
            if (canAttack(toAttack.IsAlive(),_secondaryAttack.GetCostOfAttack()))
            {
                var performedAttack = _secondaryAttack.Attack(toAttack);
                if (performedAttack)
                {
                    attacked(toAttack.GetPosition(), _secondaryAttack.GetCostOfAttack(), true);
                }
            }
        }


        void attacked(Vector3 positionOfAttacked, int amountOfResource, bool spentResource)
        {
            _toPrimaryAttack = null;
            _movement.StopMoving();
            transform.LookAt(positionOfAttacked);
            nextTimeToAttack = Time.time + _globalCooldown;
            if (spentResource)
            {
                _resourceManager.Spend(amountOfResource);
            }
            else
            {
                _resourceManager.Generate(amountOfResource);
            }
        }
        bool isInRangeToAttack(int attackRange, Vector3 toAttackPosition)
        {
            var distanceToAttack = Vector3.Distance(transform.position, toAttackPosition);
            return distanceToAttack <= attackRange;
        }

        bool canAttack( bool toAttackIsAlive, int amountOfResourceForAttack = 0)
        {
            if (!_resourceManager.HasEnough(amountOfResourceForAttack))
            {
                FindObjectOfType<AudioManager>().Play(AUDIOCLIPS.MORE_ENERGY_NEEDED);
                return false;
            }
            return !isOnCooldown();

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