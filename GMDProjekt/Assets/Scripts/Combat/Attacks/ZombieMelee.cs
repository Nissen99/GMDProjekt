using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ZombieMelee : MonoBehaviour, IAttack
{
    public int BaseDamage { get; } = 10;
    public int Range { get; } = 2;
    public float Cooldown { get; } = 0f;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {        
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Attack(IAttackable toAttack)
    {
        _animator.Play("InvigoratingStrike");
        return toAttack.Attack(BaseDamage);
    }
}
