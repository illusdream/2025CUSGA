using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[UIPanelSetting(EUILayer.Normal, 3, true, EAssetLoadMode.Resources, "Prefab/Shili/FadeImage")]
public class FadeImageUI : UIPanel
{
    public override void Close()
    {
        base.Close();
        UIPanelObject.SetActive(false);
    }
    public override void Open()
    {
        base.Open();
        UIPanelObject.SetActive(true);
    }
}
