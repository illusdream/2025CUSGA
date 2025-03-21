using System;
using ilsFramework;
using UnityEngine;

public class PlayerMoveComponent : EntityComponent,IEntityMove
{
    public override string TargetUsage => EntityComponetUsage.Moveable;

    public Rigidbody2D Rigidbody2D;
    
    public float MaxMoveSpeed = 5f;
    
    public float MoveAcceleration = 5f;
    
    public float SpeedFalloff = 0.5f;
    
    public bool IsInputMoving;

    private Vector2 finalInputMoveDir;
    
    public Vector3 GetEntityPosition()
    {
        return transform.position;
    }

    public Vector3 GetEntityRotation()
    {
        return transform.eulerAngles;
    }

    public Vector3 GetEntityVelocity()
    {
       return Rigidbody2D.velocity;
    }

    public void SetTargetVelocity(Vector3 velocity)
    {
        var finalVelocity = Vector2.ClampMagnitude(velocity, MaxMoveSpeed);
        Rigidbody2D.velocity = finalVelocity;
    }

    public void AddForce(Vector3 force)
    {
        Rigidbody2D.AddForce(force);
    }

    public override void OnInitialized(EntityHandler handler)
    {
        handler.AddEventListener(PlayerEvent.PlayerMoveCommend,EEntityEventScope.Component,ExecuteMove);
        base.OnInitialized(handler);
    }

    
    public override void OnEntityDestroy(EntityHandler handler)
    {
        handler.RemoveEventListener(PlayerEvent.PlayerMoveCommend,EEntityEventScope.Component,ExecuteMove);
        base.OnEntityDestroy(handler);
    }
    
    private void ExecuteMove(EventArgs args)
    {
        if (args is PlayerEvent.PlayerMoveCommendEventArgs moveCommendEventArgs)
        {
            finalInputMoveDir = moveCommendEventArgs.PlayerMoveDirection;
            IsInputMoving = true;
        }
    }

    public void FixedUpdate()
    {

        if (!IsInputMoving)
        {
            var tickFalloffVector = Time.fixedDeltaTime * SpeedFalloff;
            if (tickFalloffVector > Rigidbody2D.velocity.magnitude)
            {
                Rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                Rigidbody2D.velocity -= Rigidbody2D.velocity.normalized * tickFalloffVector;
            }
        }
        else
        {
            Rigidbody2D.velocity += finalInputMoveDir * (MoveAcceleration * Time.fixedDeltaTime);
            Rigidbody2D.velocity = Vector2.ClampMagnitude(Rigidbody2D.velocity, MaxMoveSpeed); 
        }
        IsInputMoving = false;
    }
}