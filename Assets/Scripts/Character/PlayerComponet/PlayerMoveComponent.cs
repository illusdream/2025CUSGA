using System;
using ilsFramework;
using Sirenix.OdinInspector;
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
    
    public bool CanBeControlled { get;set; }
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
        Rigidbody2D.velocity = velocity;
    }

    public void AddForce(Vector3 force)
    {
        AddForce(force, ForceMode2D.Impulse);
    }

    public void AddForce(Vector3 force,ForceMode2D mode = ForceMode2D.Impulse)
    {
        Rigidbody2D.AddForce(force, mode);
    }

    public override void OnInitialized(EntityHandler handler)
    {
        base.OnInitialized(handler);
    }

    
    public override void OnEntityDestroy(EntityHandler handler)
    {
        base.OnEntityDestroy(handler);
    }
    
    public void FixedUpdate()
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

        var result = Rigidbody2D.velocity + finalInputMoveDir * (MoveAcceleration * Time.fixedDeltaTime);
        if (result.magnitude <= MaxMoveSpeed && finalInputMoveDir != Vector2.zero)
        {
            Rigidbody2D.velocity = result;
            finalInputMoveDir = Vector2.zero;
        }
    }

    public void Move(Vector2 moveDir)
    {
        if (!CanBeControlled)
        {
            return;
        }
        finalInputMoveDir = moveDir;
    }
}