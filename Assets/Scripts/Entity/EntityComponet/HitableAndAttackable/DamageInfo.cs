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
       

       public float GetFinalApplyDamage()
       {
              return baseDamage;
       }
       /// <summary>
       /// 计算伤害结算之后的插值
       /// </summary>
       /// <param name="dmgInfo"></param>
       /// <param name="beHittedInfo"></param>
       /// <returns></returns>
       public static DamageInfo operator - (DamageInfo dmgInfo, BeHittedInfo beHittedInfo)
       {
              float curDamage;
              if (!beHittedInfo.IsHitted)
              {
                     curDamage = 0;
              }

              curDamage = dmgInfo.GetFinalApplyDamage() - beHittedInfo.HasBeHittedDamage;

              DamageInfo result = new DamageInfo()
              {
                     baseDamage = curDamage,
                     DamageFrom = dmgInfo.DamageFrom,
                     DamageFromSystem = dmgInfo.DamageFromSystem,
              };
              return result;
       }

       /// <summary>
       /// 伤害是有效的
       /// </summary>
       /// <returns></returns>
       public bool IsValid()
       {
              if (DamageFromSystem)
              {
                     return GetFinalApplyDamage() >0;
              }
              else
              {
                     return DamageFrom != EntityID.Empty && GetFinalApplyDamage() > 0;
              }
       }
}