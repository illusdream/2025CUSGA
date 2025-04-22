using UnityEngine;

public class BaseEntityMove : EntityComponent,IEntityMove
{
    public override string TargetUsage => EntityComponetUsage.Moveable;

    public Rigidbody2D rigidbody2D;

    public float MaxSpeed;
    
    public float Acceleration;
    
    public NumericModifierCollection AccelerationModifiers = new NumericModifierCollection();
    
    public NumericModifierCollection MaxMoveSpeedModifiers = new NumericModifierCollection();
    
    public virtual Vector3 GetEntityPosition()
    {
        return transform.position;
    }

    public virtual Vector3 GetEntityRotation()
    {
        return transform.eulerAngles;
    }

    public virtual Vector3 GetEntityVelocity()
    {
        return rigidbody2D.velocity;
    }

    public void SetTargetVelocity(Vector3 velocity)
    {
        rigidbody2D.velocity = velocity;
    }

    public void AddForce(Vector3 force, ForceMode2D mode = ForceMode2D.Impulse)
    {
        rigidbody2D.AddForce(force, mode);
    }

    public void Move(Vector2 dir)
    {
        Vector2 cVelocity = rigidbody2D.velocity;
        var preVelocity =cVelocity+ dir * AccelerationModifiers.Apply(Acceleration) * Time.fixedDeltaTime;
        
        var cMaxSpeed = MaxMoveSpeedModifiers.Apply(MaxSpeed);
        
        if (preVelocity.magnitude > cMaxSpeed && cVelocity.magnitude < cMaxSpeed)
        {
            cVelocity = cMaxSpeed * preVelocity.normalized;
        }

        if (preVelocity.magnitude < cMaxSpeed)
        {
            cVelocity = preVelocity;
        }
        
        rigidbody2D.velocity = cVelocity;
    }
    

    
}