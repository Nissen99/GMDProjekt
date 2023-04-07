
using UnityEngine;

namespace DefaultNamespace
{
    public interface IMovement
    {
        public void Move(Vector3 placeToMove);
        public void StopMoving();
    }
}
