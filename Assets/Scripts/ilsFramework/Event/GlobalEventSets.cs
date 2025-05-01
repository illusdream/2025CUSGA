using System;
using UnityEngine;

namespace ilsFramework
{
    public partial class GlobalEventSets
    {
        //ʵ�浽��һ��
        public const string ResetKey = "ResetKey";
        public const string PromptAppears = "PromptAppears";
        
        /// <summary>
        /// 点击开始按钮，准备开始游戏
        /// </summary>
        public const string OrderStartGame = "OrderStartGame";

        /// <summary>
        /// 命令游戏流程控制器 暂停游戏流程
        /// </summary>
        public const string OrderToPauseGame = "OrderToPauseGame";
        /// <summary>
        /// 命令游戏流程控制器 从暂停出继续游戏流程
        /// </summary>
        public const string OrderToResumeGame = "OrderToResumeGame";
        
        /// <summary>
        ///  命令游戏流程控制器 重新开始游戏(这个事件在主界面是没有用的)
        /// </summary>
        public const string OrderToRestartGamePlay = "OrderToRestartGame";
        /// <summary>
        /// 命令游戏流程控制器 切换至主界面，无限制
        /// </summary>
        public const string OrderToSwitchToMainMenu = "OrderToSwitchToMainMenu";
        /// <summary>
        /// 命令游戏流程控制器 进入教程场景
        /// </summary>
        public const string OrderToGuidelinesScene = "OrderToGuidelinesScene";


        public const string GameRestart = "GameRestart";
        
        /// <summary>
        /// 一局游戏结束的事件,对应的EventArgs<see cref="GameOverEventArgs"/>
        /// </summary>
        public const string GameOver = "GameOver";
        public class GameOverEventArgs: EventArgs
        {
            /// <summary>
            /// 赢的人的PlayerID，要拿实例用CharactorManager的TryGetPlayerController
            /// </summary>
            public int WinPlayerID;
            /// <summary>
            /// 赢的人的EntityID，要拿实例用CharactorManager的TryGetPlayerController
            /// </summary>
            public EntityID WinnerID;
            /// <summary>
            /// 输的人的PlayerID，要拿实例用CharactorManager的TryGetPlayerController
            /// </summary>
            public int LosePlayerID;
            /// <summary>
            /// 输的人的EntityID，要拿实例用CharactorManager的TryGetPlayerController
            /// </summary>
            public EntityID LoserID;

            public GameOverEventArgs(PlayerController winner,PlayerController loser)
            {
                WinPlayerID = (winner?.PlayerID).GetValueOrDefault(-1);
                WinnerID = (winner?.ID).GetValueOrDefault(EntityID.Empty);
                
                LosePlayerID = (loser?.PlayerID).GetValueOrDefault(-1);
                LoserID = (loser?.ID).GetValueOrDefault(EntityID.Empty);
            }
        }
        
        
        public const string PlayerSpawn = "PlayerSpawn";
        
        public class PlayerSpawnEventArgs : EventArgs
        {
            public PlayerController Controller;
            public int PlayerID;
            public SpawnSource SpawnSource;

            public PlayerSpawnEventArgs(PlayerController controller, int playerID, SpawnSource spawnSource)
            {
                Controller = controller;
                PlayerID = playerID;
                SpawnSource = spawnSource;
            }
        }
    }
}