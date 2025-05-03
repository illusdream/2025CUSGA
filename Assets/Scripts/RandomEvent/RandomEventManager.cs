using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class RandomEventManager : ManagerSingleton<RandomEventManager>,IManager,IAssemblyForeach
{
    private RandomEventConfig randomEventConfig;
    [ShowInInspector]
    private Dictionary<Type, BaseRandomEventConfig> randomEventConfigs;
    [ShowInInspector]
    private Dictionary<string,BaseRandomEvent> randomEvents;
    
    private List<string> randomEventsNeedRemove;
    
    private BiMap<ERandomEventType, Type> randomEventType_TypeMap;
    
    public float CurrentRandomEventInterval;
    
    /// <summary>
    /// 是否开启随机事件循环
    /// </summary>
    private bool _randomEventIsEnabled;

    private float RandomEventTimer;
    
    public List<ERandomEventType> RandomSelectList;
    
    public void Init()
    {
        randomEventConfig = Config.GetConfig<RandomEventConfig>();
        
        randomEventConfigs = new Dictionary<Type, BaseRandomEventConfig>();
        
        randomEvents = new Dictionary<string, BaseRandomEvent>();
        
        randomEventsNeedRemove = new List<string>();
        
        randomEventType_TypeMap = new BiMap<ERandomEventType, Type>();

        RandomSelectList = randomEventConfig.BeRandomSelectRandomEvent;
        
        CurrentRandomEventInterval = randomEventConfig.RandomEventInterval;
    }
    public void ForeachCurrentAssembly(Type[] types)
    {
        foreach (var type in types)
        {
            if (typeof(BaseRandomEvent).IsAssignableFrom(type) && !type.IsAbstract)
            {
                if (randomEventConfig.TryGetVisualEffectConfig(type.FullName,out var baseBuffConfig))
                {
                    randomEventConfigs.Add(type, baseBuffConfig);
                    
                    if (randomEventConfig.TryGetPropID(type.Name, out var propID))
                    {
                        randomEventType_TypeMap.Add((ERandomEventType)propID, type);
                    }
                } 
            }
        }
    }

    public void Update()
    {
        RandomEventCycleUpdate();
        
        foreach (var value in randomEvents.Values)
        {
            value.OnEventUpdate();
        }
        foreach (var eventValue in randomEvents.Values)
        {
            if (!eventValue.IsValid)
            {
                randomEventsNeedRemove.Add(eventValue.ID);
            }
        }
        foreach (var key in randomEventsNeedRemove)
        {
            RemoveRandomEventImmidiately(key);
        }
        randomEventsNeedRemove.Clear();
    }

    public void RandomEventCycleUpdate()
    {
        if (_randomEventIsEnabled)
        {
            RandomEventTimer += Time.deltaTime;

            if (RandomEventTimer >= CurrentRandomEventInterval)
            {
                RandomEventTimer -= CurrentRandomEventInterval;
                var type = GetCurrentRandomEventType();
                AddRandomEvent(type);
            }
        }
    }
    
    
    public void LateUpdate()
    {

    }

    public void FixedUpdate()
    {
        foreach (var value in randomEvents.Values)
        {
            value.OnEventFixedUpdate();
        }
    }

    public void OnDestroy()
    {
        ClearAllRandomEvent();
    }

    public void OnDrawGizmos()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
      
    }


    public BaseRandomEvent AddRandomEvent(ERandomEventType type)
    {
        if (randomEventType_TypeMap.TryGetRight(type,out var _type) && randomEventConfigs.TryGetValue(_type, out var baseConfig) && Activator.CreateInstance(_type) is BaseRandomEvent instance)
        {
            111.LogSelf();
            instance._config = baseConfig;
            instance.ID = _type.Name + instance.GetHashCode();
            instance.Init();
            instance.OnEventStart();
            randomEvents.Add(instance.ID, instance);
            return instance;
        }
        return null;
    }
    
    public BaseRandomEvent AddRandomEvent(Type eventType)
    {
        if (randomEventConfigs.TryGetValue(eventType, out var baseConfig) && Activator.CreateInstance(eventType) is BaseRandomEvent instance)
        {
            instance._config = baseConfig;
            instance.ID = eventType.Name + instance.GetHashCode();
            instance.Init();
            instance.OnEventStart();
            randomEvents.Add(instance.ID, instance);
            return instance;
        }
        return null;
    }
    
    public T AddRandomEvent<T>() where T : BaseRandomEvent, new()
    {
        if (randomEventConfigs.TryGetValue(typeof(T), out var baseConfig) && Activator.CreateInstance(typeof(T)) is T instance)
        {
            instance._config = baseConfig;
            instance.ID = typeof(T).Name + instance.GetHashCode();
            instance.Init();
            instance.OnEventStart();
            randomEvents.Add(instance.ID, instance);
            return instance;
        }

        return null;
    }

    public void RemoveRandomEvent(string id)
    {
        randomEventsNeedRemove.Add(id);
    }

    private void RemoveRandomEventImmidiately(string id)
    {
        if (randomEvents.TryGetValue(id,out var @event))
        {
            @event.OnEventEnd();
            @event.OnEventDestroy();
            randomEvents.Remove(id);
        }
    }

    public void ClearAllRandomEvent()
    {
        foreach (var value in randomEvents.Values)
        {
            value.OnEventEnd();
            value.OnEventDestroy();
        }
        randomEvents.Clear();
    }

    public ERandomEventType GetCurrentRandomEventType()
    {
        return  RandomSelectList.Shuffle().FirstOrDefault();
    }
    
    /// <summary>
    /// 开启游戏中随机事件的正常循环
    /// </summary>
    public void StartGameCommonRandomEventCycle()
    {
        _randomEventIsEnabled = true;
    }

    public void StopGameCommonRandomEventCycle()
    {
        _randomEventIsEnabled = false;
    }

    public void ResetGameCommonRandomEventCycle(int interval)
    {
        CurrentRandomEventInterval = interval;
    }

    public void SetCurrentRandomSelectList(List<ERandomEventType> randomEvents)
    { 
        RandomSelectList = randomEvents;
    }
    
    public void SetDefaultRandomSelectList()
    {
        RandomSelectList = randomEventConfig.BeRandomSelectRandomEvent;
    }

}