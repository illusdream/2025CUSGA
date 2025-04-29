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
        
        
        
        public const string BeOrderToStartPlaceTile = "BeOrderToStartPlaceTile";
        public const string BeOrderToEndPlaceTile = "BeOrderToEndPlaceTile";
        
        public const string BeOrderToStartBreakTile = "BeOrderToStartBreakTile";
        
        public const string BeOrderToEndBreakTile = "BeOrderToEndBreakTile";

        public class BeOrderToEndBreakTileEventArgs : EventArgs
        {
                public float BreakOrderContinueTime;
                public float startOrderTime;
                public float endOrderTime;
                public BeOrderToEndBreakTileEventArgs(float breakOrderContinueTime, float startOrderTime, float endOrderTime)
                {
                        BreakOrderContinueTime = breakOrderContinueTime;
                        this.startOrderTime = startOrderTime;
                        this.endOrderTime = endOrderTime;
                }
        }
        
        
        
        
        
        public const string BeOrderToUseProp = "BeOrderToUseProp";
        
        public const string BeHitted = "BeHitted";
        public class BeHittedEventArgs : EventArgs
        {
                public DamageInfo DamageInfo;

                public BeHittedEventArgs(DamageInfo damageInfo)
                {
                        DamageInfo = damageInfo;
                }
        }
        
        /// <summary>
        /// 玩家获取到一个新的的Prop,对应的Args：<see cref="PlayerGetNewPropEventArgs"/>
        /// </summary>
        public const string PlayerGetNewProp = "PlayerGetNewProp";

        public class PlayerGetNewPropEventArgs : EventArgs
        {
                public EntityID PlayerEntityID;
                public int PlayerID;
                public Type PropType;

                public PlayerGetNewPropEventArgs(EntityID playerEntityID, int playerID, Type propType)
                {
                        PlayerEntityID = playerEntityID;
                        PlayerID = playerID;
                        PropType = propType;
                }
        }
        
        /// <summary>
        /// 玩家使用了一个道具,对应的Args：<see cref="PlayerUsingPropEventArgs"/>
        /// </summary>
        public const string PlayerComsumeProp = "PlayerUsingProp";
        
        
        public class PlayerUsingPropEventArgs : EventArgs
        {
                public EntityID PlayerEntityID;
                public int PlayerID;
                public Type PropType;

                public PlayerUsingPropEventArgs(EntityID playerEntityID, int playerID, Type propType)
                {
                        PlayerEntityID = playerEntityID;
                        PlayerID = playerID;
                        PropType = propType;
                }
        }
                
}