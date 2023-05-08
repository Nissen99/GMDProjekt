using System.Collections;
using DefaultNamespace;
using Events;
using Spells;
using UnityEngine;
using Util;

namespace Combat.Spells
{
    public class SmokeScreen : MonoBehaviour, ISpell
    {
        private ICooldownManager _cooldownManager;
        public float Cooldown;
        public float MoveSpeedMultiplier;
        public float LenghtOfBuff;
        private IPlayerMovement _movement;
        private IHpController _hpController;
        public ParticleSystem ParticleSystem;
        private SpellUsed onSpellUsed;

        private void Awake()
        {
            onSpellUsed = new SpellUsed();
            _cooldownManager = new CooldownManager(Cooldown);
            _movement = GetComponent<IPlayerMovement>();
            _hpController= GetComponent<IHpController>();
        }
    
        public bool Cast(Vector3? positionToCast, IAttackable toAttack = null)
        {
            _cooldownManager.Use();
            _movement.IncreaseMovementSpeed(MoveSpeedMultiplier);
            _hpController.MakeImmune();
            StartCoroutine(ResetBuffs());
            var partical = Instantiate(ParticleSystem, transform.position, new Quaternion(45f, 180f,180f, 0f));
            partical.transform.SetParent(transform);
            Destroy(partical.gameObject, LenghtOfBuff);
            onSpellUsed.Invoke();
            return true;
        }

        public bool IsReady()
        {
            return _cooldownManager.IsReady();
        }

        public ICooldownManager GetCooldownManager()
        {
            return _cooldownManager;
        }

        public SpellUsed GetSpellUsedEvent()
        {
            return onSpellUsed;
        }

        IEnumerator ResetBuffs()
        {
            yield return new WaitForSeconds(LenghtOfBuff);
            _movement.DecreaseMovementSpeed(MoveSpeedMultiplier);
            _hpController.MakeDamageable();
        }
    }
}
