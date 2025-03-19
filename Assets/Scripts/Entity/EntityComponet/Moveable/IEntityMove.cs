using UnityEngine;

public interface IEntityMove
{
        public Vector3 GetEntityPosition();
        
        public Vector3 GetEntityRotation();
        
        public Vector3 GetEntityVelocity();
        
        public void SetTargetVelocity(Vector3 velocity);
        
        public void AddForce(Vector3 force);
}