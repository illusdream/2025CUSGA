using System;
using UnityEngine;

public static class PlayerEvent
{
        /// <summary>
        /// 玩家获取到移动的指令,只在玩家实例内部传输
        /// </summary>
        public const string PlayerMoveCommend = "PlayerMoveCommend";
        
        public class PlayerMoveCommendEventArgs : EventArgs
        {
                public Vector2 PlayerMoveDirection;

                public PlayerMoveCommendEventArgs(Vector2 playerMoveDirection)
                {
                        PlayerMoveDirection = playerMoveDirection;
                }
        }
        
        /// <summary>
        /// 玩家做出移动
        /// </summary>
        public const string PlayerMove = "PlayerMove";
        
        
        public const string BeOrderToBreakTile = "BeOrderToBreakTile";
        
        
        
        public const string BeOrderToPlaceTile = "BeOrderToPlaceTile";
}