using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class HpController : MonoBehaviour, IHpController
{
    [SerializeField] private int startingHp = 100;

    private int currentHp;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = startingHp;
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
