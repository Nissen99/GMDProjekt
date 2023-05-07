using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class AINavMeshMovement : MonoBehaviour, IMovement
    {
        
   private Animator _animator;
    private Vector3 _placeToMoveTo;
    [SerializeField] private int rotationSpeed;
    private NavMeshAgent _meshAgent;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _meshAgent = GetComponent<NavMeshAgent>();
    }
    
    void FixedUpdate()
    {
        if (!MovementUtil.CheckEqualWithinErrorMargin(_placeToMoveTo, transform.position))
        {
            _meshAgent.SetDestination(_placeToMoveTo);
            var movementIntent = _placeToMoveTo - transform.position;
            _animator.SetFloat(MovementUtil.animatorSpeed, movementIntent.magnitude); 
        }
        else
        {
            _animator.SetFloat(MovementUtil.animatorSpeed, 0);
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
    }
}