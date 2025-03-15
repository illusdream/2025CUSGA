public enum EEntityEventScope
{
    /// <summary>
    /// 对单个Entity广播的事件
    /// </summary>
    Entity,
    /// <summary>
    /// Entity中各组件之间消息的传递
    /// </summary>
    Component,
}