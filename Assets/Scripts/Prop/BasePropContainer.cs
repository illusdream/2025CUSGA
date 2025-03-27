public abstract class BasePropContainer : EntityComponent
{    
        public bool canUseProp;
        public sealed override string TargetUsage => EntityComponetUsage.PropContainer;

        /// <summary>
        /// 尝试使用道具
        /// </summary>
        /// <returns></returns>
        public abstract bool TryUseProp();

        /// <summary>
        /// 尝试向这个容器里输入一个道具
        /// </summary>
        /// <returns></returns>
        public abstract bool TryInputProp(BaseProp prop);
        
        public abstract bool IsFullProp();
        
        public virtual bool CanUseProp()
        {
                return canUseProp;
        }
}