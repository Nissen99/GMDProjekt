using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private int attackRange = 2;
        [CanBeNull] private GameObject player;
        private IAttackable _playerAttackable;
        private IMovement _movement;

        private IAttack _attack;

        [SerializeField] private float _globalCooldown = 2f;

        private float? nextTimeToAttack;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            _playerAttackable = player.GetComponent<IAttackable>();
            _movement = GetComponent<IMovement>();
            _attack = GetComponent<IAttack>();
        }

        // Update is called once per frame
        void Update()
        {
            if (player != null)
            {
                if (canAttack())
                {
                    var died = attackPlayer();
                    if (died)
                    {
                        player = null;
                    }
                }
                else
                {
                    moveIfNotInRange();
                }
            }
        }

        bool attackPlayer()
        {
            nextTimeToAttack = Time.time + _globalCooldown;
            _movement.StopMoving();
            return _attack.Attack(_playerAttackable);
        }

        bool canAttack()
        {
            return !isOnCooldown() && isInRangeToAttack();
        }

        bool isInRangeToAttack()
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= attackRange;
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
            if (isInRangeToAttack())
            {
                return;
            }

            _movement.Move(player.transform.position);
        }
    }
}