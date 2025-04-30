namespace ilsFramework
{
    public partial class GlobalEventSets
    {
        /// <summary>
        /// 玩家获取到一个新的的Prop,对应的Args：<see cref="PlayerEvent.PlayerGetNewPropEventArgs"/>
        /// </summary>
        public const string PlayerGetNewProp = "PlayerGetNewProp";
        
        /// <summary>
        /// 玩家消耗了一个道具,对应的Args：<see cref="PlayerEvent.PlayerGetNewPropEventArgs"/>
        /// </summary>
        public const string PlayerComsumeProp = "PlayerUsingProp";
        
        /// <summary>
        /// 玩家的道具仓库发生了更新，内部道具的顺序被修改,对应的Args：<see cref="PlayerEvent.PlayerCurrentUsePropChangedEventArgs"/>
        /// </summary>
        public const string PlayerCurrentUsePropChanged = "PlayerCurrentUsePropChanged";
    }
}
