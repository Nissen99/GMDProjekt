using System.Collections;
using System.Collections.Generic;
using Spells;
using UnityEngine;
using Util;

public class UIFirstSpell : MonoBehaviour
{
    private UIAbilityController _abilityController;
    public GameObject playerWithSpell;

    private ICooldownManager _cooldownManager;

    // Start is called before the first frame update
    void Start()
    {
        var playerHealthPotion = playerWithSpell.GetComponent<ISpell>(); //THIS will not work once more spells are added be ware
        _cooldownManager = playerHealthPotion.GetCooldownManager();
        playerHealthPotion.GetSpellUsedEvent().AddListener(onSpellUsed);
        _abilityController = GetComponent<UIAbilityController>();
    }


    void onSpellUsed()
    {
        StartCoroutine(CooldownUpdate());
    }
    IEnumerator CooldownUpdate()
    {
        do
        {
            UpdateUI();
            yield return null;
        } while (!_cooldownManager.IsReady());    
        UpdateUI();
    }
        
    void UpdateUI()
    {
        if (!_cooldownManager.IsReady())
        {
            _abilityController.SetCooldown(_cooldownManager.GetRemainingCooldown(), _cooldownManager.GetCooldown());
        }
        else
        {
            _abilityController.SetAvailable();
        }
    }
}
