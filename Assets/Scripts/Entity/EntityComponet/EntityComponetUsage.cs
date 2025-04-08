/// <summary>
/// 存储对应功能的Key以及
/// </summary>
public partial class EntityComponetUsage
{
        public const string EntityBaseCollider  = "EntityBaseCollider";
        
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
        /// 行为组件： 具体是对timeline的调用组件，通过实现攻击/挖掘等需要动画/粒子/逻辑多方面配合的时间轴。。。
        /// </summary>
        public const string ActionDirector = "ActionDirector";
        
        /// <summary>
        /// 可被攻击组件，被攻击首先会经过这个组件
        /// </summary>
        public const string Hitable = "Hitable";
        
        /// <summary>
        /// Buff组件，有这个组件的实体可以被上Buff（未实现）
        /// </summary>
        public const string Buff = "Buff";
        
        /// <summary>
        /// 道具容器，使用这个组件用以存储/使用道具
        /// </summary>
        public const string PropContainer = "PropContainer";
        
        /// <summary>
        /// 数据统计组件，通过这个组件可以快速获取或修改对应数值
        /// </summary>
        public const string EntityStat = "EntityStat";
        
        public const string EnergyContainer = "EnergyContainer";
}