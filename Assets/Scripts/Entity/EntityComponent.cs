using Sirenix.OdinInspector;
using UnityEngine;

public abstract class EntityComponent : MonoBehaviour
{
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
    
    public virtual void OnInitialized(EntityHandler handler)
    {
            
    }


    public virtual void OnEntityDestroy(EntityHandler handler)
    {
            
    }
}