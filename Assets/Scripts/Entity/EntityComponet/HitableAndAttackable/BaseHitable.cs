namespace DefaultNamespace
{
    public class BaseHitable : EntityComponent,IHitable
    {
        public override string TargetUsage =>EntityComponetUsage.Hitable;

        public virtual bool CanBeHit()
        {
            return true;
        }

        public virtual void Hit(DamageInfo damageInfo, out BeHittedInfo beHittedInfo)
        {
            beHittedInfo = default(BeHittedInfo);
        }
    }
}