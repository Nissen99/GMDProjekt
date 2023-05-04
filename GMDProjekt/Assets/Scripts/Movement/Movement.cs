using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    private Animator _animator;
    private Vector3 _placeToMoveTo;
    [SerializeField] private int speed;
    [SerializeField] private int rotationSpeed;
    private static readonly int animatorSpeed = Animator.StringToHash("Speed");
    private float _acceptedErrorMargin = 0.1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    void FixedUpdate()
    {
        if (!CheckEqualWithinErrorMargin(_placeToMoveTo, transform.position, _acceptedErrorMargin))
        {       
            var movementIntent = _placeToMoveTo - transform.position;
            movementIntent.y = 0;
            LookAt(_placeToMoveTo);
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
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

    private bool CheckEqualWithinErrorMargin(Vector3 v1, Vector3 v2, float acceptedError)
    {
        if (Mathf.Abs(v1.x - v2.x) > acceptedError)
        {
            return false;
        }

        if (Mathf.Abs(v1.y - v2.y) > acceptedError)
        {
            return false;
        }

        if (Mathf.Abs(v1.z - v2.z) > acceptedError)
        {
            return false;
        }

        return true;
    }
}
