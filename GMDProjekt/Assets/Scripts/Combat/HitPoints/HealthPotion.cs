using DefaultNamespace;
using Events;
using UnityEngine;
using Util;

namespace Combat.HitPoints
{
    public class HealthPotion : MonoBehaviour, IHealthPotion
    {
        public int HealthGainedOnUse = 750;
        public float CooldownOnPotion = 15f;
        private ICooldownManager _cooldownManager;
        private IHpController _hpController;

        private SpellUsed onSpellUsed;
        
        // I am using Awake because it is an event that other scripts will likely subscribe to in their start, so we need
        // to make sure this has happened before start.
        private void Awake()
        {
            onSpellUsed = new SpellUsed(); 
            _cooldownManager = new CooldownManager(CooldownOnPotion);
        }

        // Start is called before the first frame update
        void Start()
        {
            _hpController = GetComponent<IHpController>();
        }
        

        public void Use()
        {
            if (!_cooldownManager.IsReady())
            {
                return;   
            }
            _hpController.Heal(HealthGainedOnUse);
            _cooldownManager.Use();
            onSpellUsed.Invoke();
        }

        public SpellUsed GetSpellUsedEvent()
        {
            return onSpellUsed;
        }

        public ICooldownManager GetCooldownManager()
        {
            return _cooldownManager;
        }
    }
}
