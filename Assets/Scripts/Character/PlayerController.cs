using ilsFramework;
using UnityEngine;

public class PlayerController : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerController;

        public int PlayerID { get;private set; }
        
        
        public void Initialize(int playerID)
        {
                PlayerID = playerID;
        }

        public void ExecuteMoveCommand(Vector2 playerMoveDirection)
        {
                BroadcastEvent(PlayerEvent.PlayerMoveCommend,EEntityEventScope.Component,new PlayerEvent.PlayerMoveCommendEventArgs(playerMoveDirection));
        }
}