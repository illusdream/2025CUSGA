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

    public override void OnInitialized(EntityHandler handler)
    {
        AddEventListener(PlayerEvent.BeOrderToUseProp,EEntityEventScope.Component,Listener_BeOrderToUseProp);
        AddEventListener(PlayerEvent.HasEnoughEnergyToMakeProp,EEntityEventScope.Component,Listener_HasEnoughEnergyToMakeProp);
        base.OnInitialized(handler);
    }

    public override void OnEntityDestroy(EntityHandler handler)
    {
        RemoveEventListener(PlayerEvent.BeOrderToUseProp,EEntityEventScope.Component,Listener_BeOrderToUseProp);
        RemoveEventListener(PlayerEvent.HasEnoughEnergyToMakeProp,EEntityEventScope.Component,Listener_HasEnoughEnergyToMakeProp);
        base.OnEntityDestroy(handler);
    }

    public void Start()
    {
        propInventory ??= new Stack<BaseProp>();
    }

    public override bool TryUseProp()
    {
        if (propInventory.Count>0 &&propInventory.Peek().CanUseProp(handler))
        {
           var p =  propInventory.Pop();
           p.UseProp(handler);
           p.BeRemovedFromContainer(handler);
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

    private void Listener_HasEnoughEnergyToMakeProp(EventArgs args)
    {
        if (args is PlayerEvent.HasEnoughEnergyToMakePropEventArgs _args)
        {
            if (!IsFullProp() &&TryInputProp(PropManager.Instance.CreateTargetProp(typeof(LaserGunProp))))
            {
                _args.energyContainer.CumsumEnergy(100);
            }
        }
    }
}