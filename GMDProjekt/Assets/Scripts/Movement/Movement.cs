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
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    void FixedUpdate()
    {
        
        
        if (!MovementUtil.CheckEqualWithinErrorMargin(_placeToMoveTo, transform.position))
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


}
