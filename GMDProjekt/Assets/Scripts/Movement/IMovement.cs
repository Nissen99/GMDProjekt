
using UnityEngine;

namespace DefaultNamespace
{
    public interface IMovement
    {
        public void Move(Vector3 placeToMove);
        public void StopMoving();
        public void LookAt(Vector3 placeToLook);
 
    }
}
