public struct BeHittedInfo
{
        /// <summary>
        /// 对这个实体实际造成的伤害
        /// </summary>
        public float HasBeHittedDamage;

        /// <summary>
        /// 是否杀死这个实体
        /// </summary>
        public bool IsKilledEntity;
        
        /// <summary>
        /// 是否被击中
        /// </summary>
        public bool IsHitted;
        
        public static BeHittedInfo Default = new BeHittedInfo()
        {
            HasBeHittedDamage = 0f,
            IsKilledEntity = false,
            IsHitted = false,
        };

}