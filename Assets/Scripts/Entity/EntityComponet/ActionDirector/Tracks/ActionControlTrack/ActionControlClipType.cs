public enum EControlClipType
{
        /// <summary>
        /// 不控制timeline的执行（按线性时间执行）
        /// </summary>
        None,
        /// <summary>
        /// 循环，按次数
        /// </summary>
        LoopByTimes,
        /// <summary>
        /// 循环，按条件（条件为真则继续循环，否则退出）
        /// </summary>
        LoopByCondition,
}