using System;

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