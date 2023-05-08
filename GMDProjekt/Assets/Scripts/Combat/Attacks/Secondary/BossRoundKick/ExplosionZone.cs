using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Combat.Attacks.Secondary.BossRoundKick
{
    public class ExplosionZone : MonoBehaviour
    {
        public GameObject ExposionPartical;
        private int _damage;
        private List<GameObject> playersInZone = new List<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TAGS.PLAYER))
            {
                playersInZone.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TAGS.PLAYER))
            {
                playersInZone.Remove(other.gameObject);
            }
        }


        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        private void OnDestroy()
        {
            Debug.Log("YSADSADASDDSADSA");

            Debug.Log(playersInZone.Count);
            Instantiate(ExposionPartical, transform.position, Quaternion.identity);
            foreach (var player in playersInZone)
            {
                var attackable = player.GetComponent<IAttackable>();
                attackable.Attack(_damage);
            }
        }
    }
}