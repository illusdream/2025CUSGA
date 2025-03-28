using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Props;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerPropContainer : BasePropContainer
{
    [ShowInInspector]
    public List<BaseProp> propInventory;

    public int MaxInventorySize;
    public PlayerController playerController;
    private TimerCollection timerCollection;
    public const string UseTimeColdDownTimer = "UseTimeColdDown";
    public void Awake()
    {
        timerCollection = new TimerCollection();
    }

    public override void OnInitialized(EntityHandler handler)
    {
        canUseProp = true;
        AddEventListener(PlayerEvent.BeOrderToUseProp,EEntityEventScope.Component,Listener_BeOrderToUseProp);
        base.OnInitialized(handler);
    }

    public override void OnEntityDestroy(EntityHandler handler)
    {
        RemoveEventListener(PlayerEvent.BeOrderToUseProp,EEntityEventScope.Component,Listener_BeOrderToUseProp);
        base.OnEntityDestroy(handler);
    }

    public void Start()
    {
        propInventory ??= new List<BaseProp>();
    }

    public override bool TryUseProp()
    {
        if (propInventory.Count>0 &&propInventory.Last().CanUseProp(handler) && CanUseProp())
        {
           var p =  propInventory.Last();
           propInventory.RemoveAt(propInventory.Count - 1);
           p.UseProp(handler);
           p.BeRemovedFromContainer(handler);
           
           canUseProp = false;
           timerCollection
                .CreateTimer(p.GetUsePropColdDown(handler),1,UseTimeColdDownTimer)
                .SetOnFinish(_ =>
                {
                    canUseProp = true;
                }).Register();

           var args = new PlayerEvent.PlayerUsingPropEventArgs(ID, playerController.PlayerID, p.GetType());
           
           handler.BroadcastEvent(PlayerEvent.PlayerUsingProp,EEntityEventScope.Component,args);
           GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.PlayerUsingProp,args);
           
        }
        return false;
    }

    public override bool TryInputProp(BaseProp prop)
    {
        if (propInventory.Count < MaxInventorySize)
        {
            prop.BeAddPropContainer(handler);
            propInventory.Add(prop);
            
            var args = new PlayerEvent.PlayerGetNewPropEventArgs(ID, playerController.PlayerID, prop.GetType());
           
            handler.BroadcastEvent(PlayerEvent.PlayerGetNewProp,EEntityEventScope.Component,args);
            GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.PlayerGetNewProp,args);
            
            return true;
        }
        return false;
    }

    public override bool IsFullProp()
    {
        return propInventory.Count >= MaxInventorySize;
    }
    

    private void Listener_BeOrderToUseProp(EventArgs args)
    {
        TryUseProp();
    }

    /// <summary>
    /// 获取对应Index的Prop信息
    /// </summary>
    /// <param name="index">对应的序号(0-2)</param>
    /// <param name="prop">返回的是道具的基类</param>
    /// <param name="propConfig">道具的配置信息，图片在这个里面</param>
    /// <returns></returns>
    public bool TryGetPropInfo(int index, out BaseProp prop, out BasePropConfig propConfig)
    {
        if (index >=0 && index < propInventory.Count)
        {
            prop = propInventory[index];
            if (!PropManager.Instance.TryGetPropConfig(prop.GetType(),out propConfig))
            {
                return false;
            }
            return true;
        }
        
        prop = null;
        propConfig = null;
        return false;
    }

}