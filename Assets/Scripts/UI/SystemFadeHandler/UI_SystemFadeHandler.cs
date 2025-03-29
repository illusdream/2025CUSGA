using ilsFramework;
using UnityEngine.Playables;

[UIPanelSetting(EUILayer.Normal,1,false,EAssetLoadMode.Resources,"Prefab/UI/SystemFadeHandler")]
public class UI_SystemFadeHandler : UIPanel
{
    [AutoUIElement("TimelinePlayDirector")]
    PlayableDirector playableDirector;
    [AutoUIElement("TimelinePlayDirector")]
    PlayableInventory inventory;

    public void FadeOut(out float duration)
    {
        if (inventory.TryGetTimelineAsset("FadeOut",out var asset))
        {
            playableDirector.playableAsset = asset;
            duration = (float)asset.duration;
            playableDirector.Play();
            return;
        }
        duration = 0f;
    }
        
    public void FadeIn(out float duration)
    {
        if (inventory.TryGetTimelineAsset("FadeIn",out var asset))
        {
            playableDirector.playableAsset = asset;
            duration = (float)asset.duration;
            playableDirector.Play();
            return;
        }
        duration = 0f;
    }

    public override void Open()
    {
        UIPanelCanvasGroup.alpha = 1;
        UIPanelCanvasGroup.blocksRaycasts = false;
        UIPanelCanvasGroup.interactable = false;
    }

    public override void Close()
    {
        UIPanelCanvasGroup.alpha = 0;
        UIPanelCanvasGroup.blocksRaycasts = false;
        UIPanelCanvasGroup.interactable = false;
    }
}