using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Events;
using Items;
using UnityEngine;
using Util;

public class HpController : MonoBehaviour, IHpController
{
    public int BaseMaxHp;
    [Range(0,1f)] public float percentageOfMaxHealedPrSecond = 0.03f;
    private int maxHp;

   [SerializeField] private int currentHp;
   public HealthChangedEvent onHealthChange;
   public string DeathSoundClipName;
   public bool ShouldScaleWithDifficulty;
   public bool IsPlayer;
   public float TimeBeforeDespawnAfterDeath = 3f;
   private bool _dead;
   private Inventory _inventory;
   private bool _isImmune;
   

   // I am using Awake because it is an event that other scripts will likely subscribe to in their start, so we need
   // to make sure this has happened before start.
   private void Awake()
   {
       onHealthChange = new HealthChangedEvent(); 
   }

   // Start is called before the first frame update
    void Start()
    {
        if (ShouldScaleWithDifficulty)
        {
            scaleWithDifficulty();
        }

        _inventory = GetComponent<Inventory>();
        if (_inventory != null)
        {
            onGearChanged();
            _inventory.onMaxHealthChange.AddListener(onGearChanged);
        }
        else
        {
            maxHp = BaseMaxHp;
        }
        currentHp = maxHp;
        StartCoroutine(HealOverTime());
    }


    public void Damage(int amount)
    {
        if (_isImmune || _dead)
        {
            return;
        }
        var dead = false;
        currentHp -= amount;
        if (currentHp <= 0)
        {
            dead = true;
            currentHp = 0;
        }
        onHealthChange.Invoke(currentHp, maxHp);
        if (dead)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (_dead)
        {
            return;
        }
        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        onHealthChange.Invoke(currentHp, maxHp);
    }

    public int GetMaxHealth()
    {
        return maxHp;
    } 

    public int GetCurrentHealth()
    {
        return currentHp;
    }

    public bool IsAlive()
    {
        return !_dead;
    }

    public void MakeImmune()
    {
        _isImmune = true;
    }

    public void MakeDamageable()
    {
        _isImmune = false;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Die()
    {
        if (_dead)
        {
            return;
        }
        FindObjectOfType<AudioManager>().Play(DeathSoundClipName);
        GetComponent<Animator>().Play("Death");
        if (IsPlayer)
        {
            FindObjectOfType<GameManager>().GameLost();
        }

        _dead = true;
        Destroy(gameObject.gameObject, TimeBeforeDespawnAfterDeath);
    }

    private void scaleWithDifficulty()
    {
        var difficultyMultiplier = DifficultyManager.GetInstance().GetDifficultyMultiplier();
        BaseMaxHp *= difficultyMultiplier;
    }

    private void onGearChanged()
    {
        var vitality = _inventory.GetAmountOfVitality();
        var vitalityMultiplier = Math.Pow(vitality, 0.5) + 1;

        maxHp = (int)(BaseMaxHp * vitalityMultiplier);
        Debug.Log($"MAXHP {maxHp}");
        
    }
    
    private IEnumerator HealOverTime()
    {
        while (true)
        {
            int healAmount = (int)(maxHp * 0.03f);
            Heal(healAmount);
            yield return new WaitForSeconds(1);
        }
    }
    
}
