using System;
using ilsFramework;
using UnityEngine;

public class PlayerMoveCommend : BasePlayerCommend
{
    public Vector2 moveDirection;
    public PlayerMoveCommend(PlayerController playerController,Vector2 moveDirection) : base(playerController)
    {
        this.moveDirection = moveDirection;
    }

    public override void Execute()
    {
        if (!playerController.CanBeControlled)
        {
            return;
        }

        playerController?.ExecuteMoveCommand(moveDirection);
    }

}