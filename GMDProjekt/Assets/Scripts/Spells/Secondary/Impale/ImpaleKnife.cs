using System;
using DefaultNamespace;
using UnityEngine;

namespace Spells
{
    public class ImpaleKnife : MonoBehaviour
    {
        private int _damage;
        public float _speed;

        private void Update()
        {
            transform.Translate(UnityEngine.Vector3.forward * (_speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TAGS.ENEMY_TAG))
            {
                var enemyHit = other.gameObject.GetComponent<IAttackable>();
                enemyHit.Attack(_damage);
            }

            Destroy(gameObject);
        }

        public void SetAttack(int damage)
        {
            _damage = damage;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
    }
}