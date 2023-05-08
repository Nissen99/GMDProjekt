using System;
using Combat.Attacks;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public int RangeToStartMovingTowardsPlayer = 20;
        private bool isAggroed;
        [CanBeNull] private GameObject player;
        private IAttackable _playerAttackable;
        private IMovement _movement;

        private IAttack _primaryAttack;
        public bool IsElite;
        public bool IsBoss;

        [SerializeField] private float _globalCooldown = 2f;

        private float? nextTimeToAttack;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            _playerAttackable = player.GetComponent<IAttackable>();
            _movement = GetComponent<IMovement>();
            _primaryAttack = GetComponent<IAttack>();
            GetComponent<HpController>().onHealthChange.AddListener(ActionOnDeath);
        }

        // Update is called once per frame
        void Update()
        {
            if (player != null)
            {
                if (!isOnCooldown())
                {
                    attackPlayer();
                }
                moveIfNotInRange();
            }
        }

        bool attackPlayer()
        {
            if (!_playerAttackable.IsAlive())
            {
                return false;
            }

            nextTimeToAttack = Time.time + _globalCooldown;
            _movement.StopMoving();
            return _primaryAttack.Attack(_playerAttackable);
        }

        bool isOnCooldown()
        {
            if (nextTimeToAttack == null)
            {
                return false;
            }

            return nextTimeToAttack > Time.time;
        }

        void moveIfNotInRange()
        {
            var distanceToAttack = Vector3.Distance(transform.position, _playerAttackable.GetPosition());
            if (distanceToAttack >= _primaryAttack.GetRange() && (distanceToAttack < RangeToStartMovingTowardsPlayer || isAggroed))
            {
                isAggroed = true;
                _movement.Move(player.transform.position);
            }
        }

        void ActionOnDeath(int currentHp, int maxHp)
        {
            if (currentHp <= 0)
            {
                Died();
            }
        }

        void Died()
        {
            if (IsElite)
            {
                FindObjectOfType<GameManager>().EliteKilled();
            }

            if (IsBoss)
            {
                FindObjectOfType<GameManager>().BossKilled();
            }
        }
    }
}