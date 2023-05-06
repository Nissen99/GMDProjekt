using System.Collections;
using System.Collections.Generic;
using Combat.Attacks;
using DefaultNamespace;
using UnityEngine;

public class ZombieMelee : MonoBehaviour, IAttack
{
    public int BaseDamage = 10;
    public int Range = 2;
    public string AttackClipName;
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

    public int GetRange()
    {
        return Range;
    }

    public bool Attack(IAttackable toAttack)
    {
        if (!AttackUtil.IsInRageToAttack(Range, transform.position, toAttack.GetPosition()))
        {
            return false;
        }
        _animator.Play("InvigoratingStrike");
        toAttack.Attack(BaseDamage);
        FindObjectOfType<AudioManager>().Play(AttackClipName);
        return true;
    }
    
}
