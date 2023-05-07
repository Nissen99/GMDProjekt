using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Events;
using UnityEngine;
using Util;

public class HpController : MonoBehaviour, IHpController
{
   [SerializeField] private int maxHp = 100;

   [SerializeField] private int currentHp;
   public HealthChangedEvent onHealthChange;
   public string DeathSoundClipName;
   public bool ShouldScaleWithDifficulty;


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
        currentHp = maxHp;
    }


    public void Damage(int amount)
    {
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

    // ReSharper disable Unity.PerformanceAnalysis
    private void Die()
    {
        FindObjectOfType<AudioManager>().Play(DeathSoundClipName);
        gameObject.SetActive(false);
    }

    private void scaleWithDifficulty()
    {
        var difficultyMultiplier = DifficultyManager.GetInstance().GetDifficultyMultiplier();
        maxHp *= difficultyMultiplier;
    }
}
