using System;
using System.Collections.Generic;
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
        if (propInventory.Count>0 &&propInventory.Peek().CanUseProp())
        {
            propInventory.Pop().UseProp();
        }
        return false;
    }

    public override bool TryInputProp(BaseProp prop)
    {
        if (propInventory.Count < MaxInventorySize)
        {
            propInventory.Push(prop);
            return true;
        }
        return false;
    }

    [Button]
    public void PushNewProp(BaseProp prop)
    {
        TryInputProp(prop);
    }


    private void Listener_BeOrderToUseProp(EventArgs args)
    {
        TryUseProp();
    }
}