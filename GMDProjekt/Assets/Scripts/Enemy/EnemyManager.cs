using Combat.Attacks;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private int attackRange = 2;
        [CanBeNull] private GameObject player;
        private IAttackable _playerAttackable;
        private IMovement _movement;

        private IAttack _primaryAttack;

        [SerializeField] private float _globalCooldown = 2f;

        private float? nextTimeToAttack;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            _playerAttackable = player.GetComponent<IAttackable>();
            _movement = GetComponent<IMovement>();
            _primaryAttack = GetComponent<IAttack>();
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
                else
                {
                    moveIfNotInRange();
                }
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
            if (AttackUtil.IsInRageToAttack(attackRange, transform.position, player.transform.position))
            {
                return;
            }

            _movement.Move(player.transform.position);
        }
    }
}