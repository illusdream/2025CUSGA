using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthComponent : EntityComponent,IEntityHealth
{
    public override string TargetUsage => EntityComponetUsage.Health;
        
    /// <summary>
    /// 血量资源，按顺序触发，如果没有血量资源或者是只有一个血量资源但当前血量为0，说明血量归0
    /// </summary>
    [SerializeField]
    public SerializableDictionary<EHealthSourceType,HealthSource> healthSources;

    public float MaxHealth;
        
    public float CurrentHealth;
        
    public void Start()
    {
        healthSources ??=new SerializableDictionary<EHealthSourceType,HealthSource>();
    }

    public override void OnInitialized(EntityHandler handler)
    {
        base.OnInitialized(handler);
    }

    public bool CanBeHit()
    {
        return healthSources.Count > 0 && healthSources[0].CanBeHit();
    }

    public void Hit(DamageInfo damageInfo, out BeHittedInfo beHittedInfo)
    {
        if (CanBeHit())
        {
            healthSources[0].Hit(damageInfo, out beHittedInfo);
        }
        beHittedInfo = BeHittedInfo.Default;
    }

    public int GetMaxHealth()
    {
        return (int)MaxHealth;
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public float GetHealthPercent()
    {
        return CurrentHealth / MaxHealth;
    }

    public bool TryAddHealthSource(EHealthSourceType healthSourceType, HealthSource healthSource)
    {
       return healthSources.TryAdd(healthSourceType, healthSource);
    }

    public bool RemoveHealthSource(EHealthSourceType healthSourceType)
    {
        return healthSources.Remove(healthSourceType);
    }

    public bool TryGetHealthSource(EHealthSourceType healthSourceType, out HealthSource healthSource)
    {
        return healthSources.TryGetValue(healthSourceType, out healthSource);
    }

    public bool ContainsHealthSource(EHealthSourceType healthSourceType)
    {
        return healthSources.ContainsKey(healthSourceType);
    }

    public void Update()
    {
        foreach (var healthSource in healthSources.Values)
        {
            MaxHealth += healthSource.CurrentMaxHealth;
            CurrentHealth += healthSource.CurrentHealth;
        }
    }

    public override void OnEntityDestroy(EntityHandler handler)
    {
        base.OnEntityDestroy(handler);
    }
}