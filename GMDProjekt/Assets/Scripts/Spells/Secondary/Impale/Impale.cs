using Combat.Attacks;
using DefaultNamespace;
using UnityEngine;

namespace Spells.Impale
{
    public class Impale : MonoBehaviour, ISecondary
    {
        public int Range { get; } = 15;
        public int BaseDamge = 40;
        public float Cooldown { get; }
        public float TimeBeforeKnifeDespawns = (float)2.5;
        public float SpeedOfKnifes = 15;

        public ImpaleKnife ImpaleKnife;
        public bool Cast(Vector3? positionToCast, IAttackable toAttack = null)
        {
            if (toAttack == null)
            {
                return false;
            }

            var toAttackPosition = toAttack.GetPosition();
            if (AttackUtil.IsInRageToAttack(Range, transform.position, toAttackPosition))
            {
                Vector3 direction = toAttackPosition - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                Quaternion leftRotation = Quaternion.AngleAxis(-15, Vector3.up) * rotation;
                Quaternion rightRotation = Quaternion.AngleAxis(15, Vector3.up) * rotation;
                createKnife(rotation);
                createKnife(leftRotation);
                createKnife(rightRotation);
                return true;
            }

            return false;
        }

        //Setting the Damage and Speed of knifes on Impale rather than the prefab, as that is what change the gameplay
        private void createKnife(Quaternion rotation)
        {
            var knife = Instantiate(ImpaleKnife,
                new Vector3(transform.position.x, (float)0.5, transform.position.z), rotation);
            knife.SetAttack(BaseDamge);
            knife.SetSpeed(SpeedOfKnifes);
            Destroy(knife, TimeBeforeKnifeDespawns);
        }
    }
}