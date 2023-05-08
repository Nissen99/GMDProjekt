using DefaultNamespace;
using UnityEngine;

namespace Movement
{
    public class Movement : MonoBehaviour, IPlayerMovement
    {
        private Animator _animator;
        private Vector3 _placeToMoveTo;
        [SerializeField] private int speed;
        [SerializeField] private int rotationSpeed;
        private static readonly int animatorSpeed = Animator.StringToHash("Speed");
        private float _movementSpeedMultiplier;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    
        void FixedUpdate()
        {
        
        
            if (!MovementUtil.CheckEqualWithinErrorMargin(_placeToMoveTo, transform.position))
            {       
                var movementIntent = _placeToMoveTo - transform.position;
                movementIntent.y = 0;
                LookAt(_placeToMoveTo);
                transform.Translate(Vector3.forward * (speed*(_movementSpeedMultiplier+1) * Time.deltaTime));
                _animator.SetFloat(animatorSpeed, movementIntent.magnitude); 
            }
            else
            {
                _animator.SetFloat(animatorSpeed, 0);
            } 
        }


        public void Move(Vector3 placeToMove)
        {
            _placeToMoveTo = placeToMove;
        }

        public void StopMoving()
        {
            _placeToMoveTo = transform.position;
        }

        public void LookAt(Vector3 placeToLook)
        { 
            Vector3 direction = (placeToLook - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        public void IncreaseMovementSpeed(float multiplier)
        {
            _movementSpeedMultiplier += multiplier;
        }

        public void DecreaseMovementSpeed(float multiplier)
        {
            _movementSpeedMultiplier -= multiplier;
        }
    }
}
