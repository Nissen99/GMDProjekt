using UnityEngine;

namespace Util
{
    public class CooldownManager : ICooldownManager
    {
        private float _cooldown;
        private float _nextTimeAvailable;

        public CooldownManager(float cooldown)
        {
            _cooldown = cooldown;
        }

        public bool IsReady()
        {
            return Time.time > _nextTimeAvailable;
        }

        public void Use()
        {
            _nextTimeAvailable = Time.time + _cooldown;
        }

        public float GetCooldown()
        {
            return _cooldown;
        }

        public float GetRemainingCooldown()
        {
            return _nextTimeAvailable - Time.time;
        }
    }
}