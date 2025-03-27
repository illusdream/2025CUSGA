using System;
using System.Diagnostics.CodeAnalysis;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class EntityComponent : MonoBehaviour
{
    [NotNull]
    public EntityHandler handler;
    [HideInInspector]
    public EntityID ID { get; private set; }
    
    [ShowInInspector]
    public virtual string TargetUsage { get;protected set; }
    
    [Title("$GetUsageKey",titleAlignment:TitleAlignments.Centered)]
    [HideLabel]
    [PropertyOrder(int.MinValue)]
    [VerticalGroup("Usage")]
    [ShowInInspector]
    private EntityTopTitle targetUsage;
   
    [VerticalGroup("Usage")]
    [Button(SdfIconType.FileCode,name:"复制用途")]
    private void CopyReference()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = TargetUsage;
        textEditor.OnFocus();
        textEditor.Copy();
    }
    
    public string GetUsageKey()
    {
        return TargetUsage;
    }
        
    [DisableInInlineEditors]
    private struct EntityTopTitle
    {
            
    }

    public void SetID(EntityID id)
    {
        ID = id;
    }
    /// <summary>
    /// 实体内的初始化
    /// </summary>
    /// <param name="handler"></param>
    public virtual void OnInitialized(EntityHandler handler)
    {
        
    }

    /// <summary>
    /// 当实体销毁时触发
    /// </summary>
    /// <param name="handler"></param>
    public virtual void OnEntityDestroy(EntityHandler handler)
    {
            
    }

    public void AddEventListener(string eventType, EEntityEventScope scope, params Action<EventArgs>[] action)
    {
        handler?.AddEventListener(eventType, scope, action);
    }

    public void RemoveEventListener(string eventType, EEntityEventScope scope, params Action<EventArgs>[] action)
    {
        handler?.RemoveEventListener(eventType, scope, action);
    }

    public void BroadcastEvent(string eventType, EEntityEventScope scope, EventArgs args)
    {
        handler?.BroadcastEvent(eventType, scope, args);
    }
    
    public SpawnSource SpawnEntityBySelf()
    {
        return handler.SpawnEntityBySelf();
    }
}