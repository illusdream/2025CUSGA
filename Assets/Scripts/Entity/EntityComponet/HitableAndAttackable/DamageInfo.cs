public struct DamageInfo
{
       public float baseDamage;

       public EntityID DamageFrom;

       public bool DamageFromSystem;
       
       public static DamageInfo BuildDamageInfo(float baseDamage, EntityID damageFrom)
       {
              return new DamageInfo()
              {
                     baseDamage = baseDamage,
                     DamageFrom = damageFrom,
                     DamageFromSystem = false,
              };
       }

       public static DamageInfo BuildDamageInfoBySystem(float baseDamage)
       {
              return new DamageInfo()
              {
                     baseDamage = baseDamage,
                     DamageFrom = EntityID.Empty,
                     DamageFromSystem = true
              };
       }
}