
using UnityEngine;

namespace DefaultNamespace
{
    public interface IPlayerMovement
    {
        public void Move(Vector3 placeToMove);
        public void StopMoving();
    }
}
