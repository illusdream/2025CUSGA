using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class PropInterfaceSetterCollection : IEnumerable<PropInterfaceSetter>
{
    [SerializeField]
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)]
    private SerializableDictionary<EPropInterfaceType, PropInterfaceSetter> _propInterfaceSetters;

    public List<PropInterfaceSetter> showing => this._propInterfaceSetters.Values.ToList();
    [Button]
    public void AddSetter(EPropInterfaceType propInterfaceType)
    {
        if (!_propInterfaceSetters.ContainsKey(propInterfaceType))
        {
            _propInterfaceSetters.Add(propInterfaceType, GetDetailSetter(propInterfaceType));
        }
    }

    public PropInterfaceSetter GetDetailSetter(EPropInterfaceType propInterfaceType)
    {
        switch (propInterfaceType)
        {
            case EPropInterfaceType.IPropSpawnEntity:
                return new PropSpawnEntitySetter();
                break;
            case EPropInterfaceType.IPropVisualControl:
                return new PropVisualControlSetter();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(propInterfaceType), propInterfaceType, null);
        }
    }

    IEnumerator<PropInterfaceSetter> IEnumerable<PropInterfaceSetter>.GetEnumerator()
    {
        return this._propInterfaceSetters.Values.GetEnumerator();
    }

    public IEnumerator GetEnumerator()
    {
        return  this._propInterfaceSetters.Values.GetEnumerator();
    }
}