using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerController;

        [ShowInInspector]
        public int PlayerID { get;private set; }

        
        public void Initialize(int playerID)
        {
                PlayerID = playerID;
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