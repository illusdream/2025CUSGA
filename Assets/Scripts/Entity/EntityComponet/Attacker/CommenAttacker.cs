using DefaultNamespace;
using ilsFramework;

public class CommenAttacker : BaseAttacker
{
    public float Damage;


    public override void OnInitialized(EntityHandler handler)
    {
        base.OnInitialized(handler);
    }

    public override void Attack(EntityHandler target)
    {
        if (target.TryGetComponet(EntityComponetUsage.Hitable, out BaseHitable hitable))
        {
            var damageInfo = GetDamageInfo();
            hitable.Hit(damageInfo,out var beHittedInfo);
            if (beHittedInfo.IsHitted)
            {
                InvokeOnAttack(target,damageInfo,beHittedInfo);
            }
        }
    }

    public virtual DamageInfo GetDamageInfo()
    {
        return DamageInfo.BuildDamageInfo(Damage,ID);
    }
}