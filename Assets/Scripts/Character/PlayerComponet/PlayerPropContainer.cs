using System;
using System.Collections.Generic;
using ilsFramework;
using Props;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerPropContainer : BasePropContainer
{
    [ShowInInspector]
    public Stack<BaseProp> propInventory;

    public int MaxInventorySize;
    
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
        propInventory ??= new Stack<BaseProp>();
    }

    public override bool TryUseProp()
    {
        if (propInventory.Count>0 &&propInventory.Peek().CanUseProp(handler) && CanUseProp())
        {
           var p =  propInventory.Pop();
           p.UseProp(handler);
           p.BeRemovedFromContainer(handler);
           
           canUseProp = false;
           timerCollection
                .CreateTimer(p.GetUsePropColdDown(handler),1,UseTimeColdDownTimer)
                .SetOnFinish(_ =>
                {
                    canUseProp = true;
                }).Register();
        }
        return false;
    }

    public override bool TryInputProp(BaseProp prop)
    {
        if (propInventory.Count < MaxInventorySize)
        {
            prop.BeAddPropContainer(handler);
            propInventory.Push(prop);
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
    [Button]
    public void GetLaserGun()
    {
        TryInputProp(PropManager.Instance.CreateTargetProp(typeof(LaserGunProp)));
    }

}