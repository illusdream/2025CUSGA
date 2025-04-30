using UnityEngine;

namespace Props
{
    public class SwarmMissileMove : BaseEntityMove
    {
        public float MinSpeed =3;

        public float slerpValue = 0.1f;
        public override void Move(Vector2 dir)
        {
            var cvelocity = GetEntityVelocity();
            var cMaxVelocity = MaxMoveSpeedModifiers.Apply(MaxSpeed);
            var preVelocity = Vector3.Slerp(cvelocity, dir.normalized*cMaxVelocity, slerpValue);
            
            rigidbody2D.velocity = preVelocity;
        }
    }
}