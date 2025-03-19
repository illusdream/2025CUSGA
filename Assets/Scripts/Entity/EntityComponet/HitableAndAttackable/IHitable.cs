public interface IHitable
{
        /// <summary>
        /// 是否可以被攻击，如果返回false，即使调用<see cref="Hit"/>也不会被攻击
        /// </summary>
        /// <returns></returns>
        public bool CanBeHit();
        
        /// <summary>
        /// 具体的被攻击函数
        /// </summary>
        /// <param name="damageInfo">攻击的具体消息</param>
        /// <param name="beHittedInfo">被攻击后返回的信息</param>
        public void Hit(DamageInfo damageInfo,out BeHittedInfo beHittedInfo);
}