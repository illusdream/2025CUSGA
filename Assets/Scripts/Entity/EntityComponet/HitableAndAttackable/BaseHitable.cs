public class BaseHitable : EntityComponent,IHitable
{
    public bool _canBeHit = true;
        
    public override string TargetUsage =>EntityComponetUsage.Hitable;

    public virtual bool CanBeHit()
    {
        return _canBeHit;
    }

    public virtual void Hit(DamageInfo damageInfo, out BeHittedInfo beHittedInfo)
    {
        beHittedInfo = default(BeHittedInfo);
    }
}