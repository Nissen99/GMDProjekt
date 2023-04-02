using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Animator _animator;
    [SerializeField] private int speed;
    private static readonly int animatorSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
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
        _animator.SetFloat(animatorSpeed, _playerInput.MovementIntent.magnitude);

        gameObject.transform.Translate(_playerInput.MovementIntent * (speed * Time.deltaTime));
 
    }
}
