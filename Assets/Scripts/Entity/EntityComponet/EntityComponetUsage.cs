/// <summary>
/// 存储对应功能的Key以及
/// </summary>
public partial class EntityComponetUsage
{
        /// <summary>
        /// 移动组件，保存物体的位置与速度（均为vector3），旋转（三维空间的旋转）
        /// 使用这个组件来操控Entity进行移动
        ///  基础实现组件：   
        /// </summary>
        public const string Moveable = "Moveable";
        
        /// <summary>
        /// 血量组件，保存实体的血量，
        /// 使用这个组件对Entity的血量进行操作，实现护盾等功能也在这个组件中
        /// </summary>
        public const string Health = "Health";
        
        /// <summary>
        /// 攻击组件，可以通过这个组件实现攻击功能
        /// </summary>
        public const string AttackAble = "Attackable";
        
        /// <summary>
        /// 可被攻击组件，被攻击首先会经过这个组件
        /// </summary>
        public const string Hitable = "Hitable";
        
        /// <summary>
        /// Buff组件，有这个组件的实体可以被上Buff
        /// </summary>
        public const string Buff = "Buff";
        
        /// <summary>
        /// 使用这个组件触发摧毁方块的功能
        /// </summary>
        public const string BreakTile = "BreakTile";
        
        /// <summary>
        /// 使用这个组件触发放置方块功能
        /// </summary>
        public const string PlaceTile = "PlaceTile";
}