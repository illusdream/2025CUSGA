using System;
using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;

public class BaseBuffContainer : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.Buff;

        [ShowInInspector]
        public Dictionary<EBuffType,BaseBuff> buffs = new Dictionary<EBuffType, BaseBuff>();

        public event Action<EBuffType> OnBuffAdded;
        public event Action<EBuffType> OnBuffRemoved;

        public EBuffTag IgnoreBuffTag = EBuffTag.None;
        
        public List<BaseBuff> buffcache = new List<BaseBuff>();

        public void Start()
        {
                IgnoreBuffTag = EBuffTag.None;
        }

        [Button]
        public virtual void AddBuff(EBuffType buffType)
        {
                if (IgnoreBuffTag != EBuffTag.None &&BuffManager.Instance.CheckBuffHasTag(buffType,IgnoreBuffTag))
                {
                        return;
                }
                
                if (buffs.TryGetValue(buffType, out BaseBuff buff))
                {
                        buff.ResetBuffTimer();
                }
                else
                {
                        var instance = BuffManager.Instance.CreateInstance(buffType);
                        if (instance != null)
                        {
                                instance.BuffName = buffType;
                                instance.AddBuff(handler);
                                buffs.Add(buffType, instance);
                                OnBuffAdded?.Invoke(buffType);
                        }
                }

        }
        [Button]
        public virtual void RemoveBuff(EBuffType buffType)
        {
                if (!buffs.TryGetValue(buffType, out var buff)) return;
                buff.RemoveBuff(handler);
                OnBuffRemoved?.Invoke(buffType);
                buffs.Remove(buffType);
        }

        public virtual void RemoveBuff(string buffName)
        {

        }

        public virtual bool HasBuff(EBuffType buffType)
        {
                return buffs.ContainsKey(buffType);
        }

        public virtual void Update()
        {

        }

        public void FixedUpdate()
        {
                buffcache.Clear();
                foreach (var buff in buffs.Values)
                {
                        buffcache.Add(buff); 
                }
                foreach (var value in buffcache)
                {
                        if (!value.IsExist)
                        {
                                RemoveBuff(value.BuffName);
                        }
                }
                
                foreach (var value in buffs.Values)
                {
                        value.BuffTick(handler);
                }
        }
}