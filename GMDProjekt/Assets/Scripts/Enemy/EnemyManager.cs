using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager: MonoBehaviour 
    {
        [SerializeField] private int attackRange = 2;
        private GameObject player;

        private IMovement _movement;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            _movement = GetComponent<IMovement>();

        }

        // Update is called once per frame
        void Update()
        {
            if (canAttack())
            {
                attackPlayer();
            }
            else
            {
                _movement.Move(player.transform.position);
            }
        }

        void attackPlayer()
        {
            _movement.StopMoving();
        }

        bool canAttack()
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= attackRange;
        }
    }
}