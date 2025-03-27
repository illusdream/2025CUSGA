using System;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerController;

        [ShowInInspector]
        public int PlayerID { get;private set; }

        public float EnergyCanBeComeProp =100;
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
                BroadcastEvent(PlayerEvent.PlayerMoveCommend,EEntityEventScope.Component,new PlayerEvent.PlayerMoveCommendEventArgs(playerMoveDirection));
        }

        public bool IsAlive()
        {
                if (handler.TryGetComponet(EntityComponetUsage.Health,out BaseHealthComponent health))
                {
                     return   health.GetCurrentHealth() > 0;
                }
                return false;
        }
        
        
        
}