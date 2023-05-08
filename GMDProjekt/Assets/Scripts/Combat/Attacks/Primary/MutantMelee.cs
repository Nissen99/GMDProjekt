using System.Collections;
using System.Collections.Generic;
using Combat.Attacks;
using DefaultNamespace;
using UnityEngine;
using Util;

public class MutantMelee : MonoBehaviour, IPrimaryAttack
{
    public int BaseDamage = 20;
    public int Range = 3;
    public string AttackClipName;
    private Animator _animator;
    public bool ShouldScaleWithDifficulty;
    private int _damage;
    
    
    // Start is called before the first frame update
    void Start()
    {        
        _animator = GetComponent<Animator>();
        if (ShouldScaleWithDifficulty)
        {
            scaleWithDifficulty();
        }
        else
        {
            _damage = BaseDamage;
        }
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
        _animator.Play("MutantPunch");
        toAttack.Attack(_damage);
        FindObjectOfType<AudioManager>().Play(AttackClipName);
        return true;
    }

    public int GetResourceGeneratedPerAttack()
    {
        return 0;
    }
    
    private void scaleWithDifficulty()
    {
        var difficultyMultiplier = DifficultyManager.GetInstance().GetDifficultyMultiplier();
        _damage = difficultyMultiplier * BaseDamage;
    }
}
