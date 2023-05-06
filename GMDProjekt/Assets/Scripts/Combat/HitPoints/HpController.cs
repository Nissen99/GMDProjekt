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

    // Update is called once per frame
    void Update()
    {
        
    } 
    public void Damage(int amount)
    {
        currentHp -= amount;
        onHealthChange.Invoke(currentHp, maxHp);
        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
    
}
