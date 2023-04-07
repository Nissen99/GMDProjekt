using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;

public class InvigoratingStrike : MonoBehaviour, IAttack
{
    [SerializeField] private int baseDamage;
    public int AttackRange { get; } = 2;

    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack(IAttackable toAttack)
    {
        _animator.Play("InvigoratingStrike");
        toAttack.Attack(baseDamage);
    }
    
}

