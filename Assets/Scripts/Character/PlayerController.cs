using System;
using ilsFramework;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerController;

        public SpriteRenderer spriteRenderer;
        
        public Animator animator;
        
        public Rigidbody2D rigidbody2D;
        
        [ShowInInspector]
        public int PlayerID { get;private set; }

        public float EnergyCanBeComeProp =100;
        
        public bool CanBeControlled { get;private set; }
        
        
        
        
        public void Initialize(int playerID)
        {
                PlayerID = playerID;
        }

        public void Update()
        {
                

        }

        public void FixedUpdate()
        {
                if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer,out PlayerEnergyContainer playerEnergyContainer)
                    && handler.TryGetComponet(EntityComponetUsage.PropContainer,out BasePropContainer playerPropContainer))
                {
                        if (playerEnergyContainer.CurrentEnergy > EnergyCanBeComeProp && !playerPropContainer.IsFullProp())
                        {
                                playerPropContainer.TryInputProp(PropManager.Instance.CreateRandomProp());
                                playerEnergyContainer.CumsumEnergy(EnergyCanBeComeProp);
                        }
                }
        }

        public void ExecuteMoveCommand(Vector2 playerMoveDirection)
        {
                var x = math.sign(playerMoveDirection.x);
                var y = math.sign(playerMoveDirection.y);
                spriteRenderer.flipX = x < 0;
                animator?.SetFloat("XSpeed",Mathf.Abs(x));
                animator?.SetFloat("YSpeed",y);
                BroadcastEvent(PlayerEvent.PlayerMoveCommend,EEntityEventScope.Component,new PlayerEvent.PlayerMoveCommendEventArgs(playerMoveDirection));
        }

        public void ExecuteStopMoveCommand()
        {
                animator?.SetFloat("XSpeed",0);
                animator?.SetFloat("YSpeed",0);  
        }

        public bool IsAlive()
        {
                if (handler.TryGetComponet(EntityComponetUsage.Health,out BaseHealthComponent health))
                {
                     return   health.GetCurrentHealth() > 0;
                }
                return false;
        }

        public void SetCanBeControlled(bool canBeControlled)
        {
                CanBeControlled = canBeControlled;
        }

        public void SetPlayerSpriteColor(Color color)
        {
                spriteRenderer.color = color;
        }
        
}