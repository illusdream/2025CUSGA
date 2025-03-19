public interface IAttackable
{
        /// <summary>
        /// 是否可以攻击，如果返回false，调用Attack也不会有任何效果（直接返回）
        /// </summary>
        public void CanAttack();
        
        /// <summary>
        /// 具体的攻击方法，如何攻击取决于该实体
        /// </summary>
        public void Attack();
}