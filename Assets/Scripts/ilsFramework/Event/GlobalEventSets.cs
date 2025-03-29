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
    }
}