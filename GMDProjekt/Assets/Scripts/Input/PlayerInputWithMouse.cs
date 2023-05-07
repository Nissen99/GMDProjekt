using Combat.AttackManager;
using Combat.HitPoints;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInputWithMouse : MonoBehaviour
    {
        //private Vector3? _latestMovementClick;
        [SerializeField] private GameObject toSpawnWhenClicked;
        private delegate void ResetIntents();

        private IMovement _movement;
        private IAttackManager _attackManager;
        private IHealthPotion _healthPotion;
    
        private ResetIntents _resetIntents;
        // Start is called before the first frame update
        void Start()
        {
            _movement = GetComponent<IMovement>();
            _attackManager = GetComponent<IAttackManager>();
            _healthPotion = GetComponent<IHealthPotion>();
            _resetIntents = resetIntent;
        }
    
        // Update is called once per frame
        void Update()
        {

        }

        void OnPrimary(InputValue value)
        {
            _resetIntents.Invoke();
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag(TAGS.GROUND_TAG))
                {
                    MovePlayer(hit.point);
                    Instantiate(toSpawnWhenClicked, new Vector3(hit.point.x, hit.point.y + 0.0f, hit.point.z), Quaternion.identity);
                } 
                
                if (hit.transform.CompareTag(TAGS.WALL_TAG))
                {
                    // hit a wall, find the floor position behind the wall
                    if (Physics.Raycast(hit.point, ray.direction , out RaycastHit floorHit))
                    {
                        MovePlayer(floorHit.point);
                        Instantiate(toSpawnWhenClicked, new Vector3(floorHit.point.x, floorHit.point.y + 0.0f, floorHit.point.z), Quaternion.identity);
                    }
                }
            
                if (hit.transform.CompareTag(TAGS.ENEMY_TAG))
                {
                    var enemy = hit.transform.GetComponent<IAttackable>();
                    _attackManager.PrimaryAttack(enemy);
                    _resetIntents += _attackManager.StopPrimaryAttackIntent;
                }
            }
        }

        void OnSecondary(InputValue value)
        {
            var hit = actionTaken();
            if (hit == null)
            {
                return;
            }
            if (hit.Value.transform.CompareTag(TAGS.ENEMY_TAG))
            {
                _attackManager.SecondaryAttack(hit.Value.transform.GetComponent<IAttackable>());
            }
        }

        void OnPotion(InputValue value)
        {
            _healthPotion.Use();
        }

        private RaycastHit? actionTaken()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit;
            }

            return null;
        }

        void MovePlayer(Vector3 targetPosition)
        {
            var _latestMovementClick = new Vector3(targetPosition.x, 0, targetPosition.z);
            _movement.Move(_latestMovementClick); 
        }

        void resetIntent()
        {
            //  _latestMovementClick = null;
            _resetIntents = resetIntent;
        }
    }
}

