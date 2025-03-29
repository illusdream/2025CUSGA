using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[UIPanelSetting(EUILayer.Lower, 0, true, EAssetLoadMode.Resources, "Prefab/Shili/OnOpenGame")]
public class OnOpenGameUI : UIPanel
{
    //把两个方块放到玩家头上
    [AutoUIElement("Countdown")]
    private Text countDownTime;
    private float countdown;
    private bool isTimeCounts;
    public override void Open()
    {
        base.Open();
        countdown = 3f;
        isTimeCounts = true;
    }
    public override void Close()
    {
        base.Close();
        
    }
    public override void Update()
    {
        base.Update();
        countdown -= Time.deltaTime;
        countDownTime.text = Math.Truncate(countdown).ToString();
        if (isTimeCounts && countdown <= 0)
        {
            isTimeCounts = false;
            //UIManager.Instance.GetUIPanel<OnOpenGameNextUI>().Open();
            Close();
        }
    }
}
