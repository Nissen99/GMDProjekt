using System.Collections;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using Combat.HitPoints;
using UnityEngine;
using Util;

namespace UI
{
    public class UIPotion : MonoBehaviour
    {
        private UIAbilityController _abilityController;
        public GameObject playerWithPotion;

        private ICooldownManager _cooldownManager;

        // Start is called before the first frame update
        void Start()
        {
            var playerHealthPotion = playerWithPotion.GetComponent<IHealthPotion>();
            _cooldownManager = playerHealthPotion.GetCooldownManager();
            playerHealthPotion.GetSpellUsedEvent().AddListener(onPotionUsed);
            _abilityController = GetComponent<UIAbilityController>();
        }

        void onPotionUsed()
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
}