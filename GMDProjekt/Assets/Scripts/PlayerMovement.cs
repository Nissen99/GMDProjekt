using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private IPlayerInput _playerInput;
    private Animator _animator;
    [SerializeField] private int speed;
    [SerializeField] private int rotationSpeed;
    private static readonly int animatorSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _playerInput = GetComponent<IPlayerInput>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            //Animation
            if (_playerInput.MovementIntent != new Vector3(0, 0, 0))
            {
                var rotation = Quaternion.LookRotation(_playerInput.MovementIntent);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * (speed * Time.deltaTime));
                _animator.SetFloat(animatorSpeed, _playerInput.MovementIntent.magnitude);

            }
            else
            {
                _animator.SetFloat(animatorSpeed, 0);
                
            }

    }
}
