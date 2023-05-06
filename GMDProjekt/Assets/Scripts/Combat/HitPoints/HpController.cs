using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Events;
using UnityEngine;

public class HpController : MonoBehaviour, IHpController
{
   [SerializeField] private int maxHp = 100;

   [SerializeField] private int currentHp;
   public HealthChangedEvent onHealthChange;
   public string DeathSoundClipName;

   //Unity does not allow me to show properties in the inspector, so had to do this work around :shrug: 
   public int MaxHp
   {
       get { return maxHp; }
       set { maxHp = value; }
   }
   public int CurrentHp
   {
       get { return currentHp; }
       set { currentHp = value; }
   }

   // I am using Awake because it is an event that other scripts will likely subscribe to in their start, so we need
   // to make sure this has happened before start.
   private void Awake()
   {
       onHealthChange = new HealthChangedEvent(); 
   }

   // Start is called before the first frame update
    void Start()
    {
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

    // ReSharper disable Unity.PerformanceAnalysis
    private void Die()
    {
        FindObjectOfType<AudioManager>().Play(DeathSoundClipName);
        gameObject.SetActive(false);
    }
    
}
