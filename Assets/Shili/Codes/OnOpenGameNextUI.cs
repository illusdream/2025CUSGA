using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[UIPanelSetting(EUILayer.Lower, 0, true, EAssetLoadMode.Resources, "Prefab/Shili/OnOpenGameNext")]
public class OnOpenGameNextUI : UIPanel
{
    //应该要写一些数字更新啥的，以及把数字放到玩家头上
    public override void Open()
    {
        base.Open();
        UIPanelObject.SetActive(true);
    }
    public override void Close()
    {
        base.Close();
        UIPanelObject.SetActive(false);
    }
}
