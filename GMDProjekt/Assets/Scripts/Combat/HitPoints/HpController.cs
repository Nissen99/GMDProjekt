using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class HpController : MonoBehaviour, IHpController
{
   [SerializeField] private int maxHp = 100;

   [SerializeField] private int currentHp;
    
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
   // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    public bool Damage(int amount)
    {
        currentHp -= amount;
        if (currentHp <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
    
}
