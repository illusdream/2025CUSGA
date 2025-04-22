using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class BaseBuffContainer : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.Buff;

        [ShowInInspector]
        public Dictionary<string,BaseBuff> buffs = new Dictionary<string, BaseBuff>();

        public List<BaseBuff> buffcache = new List<BaseBuff>();
        
        [Button]
        public virtual void AddBuff(EBuffType buffType)
        {
                if (buffs.TryGetValue(buffType.ToString(), out BaseBuff buff))
                {
                                buff.ResetBuffTimer();
                }
                else
                {
                        var instance = BuffManager.Instance.CreateInstance(buffType);
                        if (instance != null)
                        {
                                instance.BuffName = buffType.ToString();
                                instance.AddBuff(handler);
                                buffs.Add(buffType.ToString(), instance);
                        }
                }

        }
        [Button]
        public virtual void RemoveBuff(EBuffType buffType)
        {
                RemoveBuff(buffType.ToString());
        }

        public virtual void RemoveBuff(string buffName)
        {
                if (!buffs.TryGetValue(buffName, out var buff)) return;
                buff.RemoveBuff(handler);
                buffs.Remove(buffName);
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