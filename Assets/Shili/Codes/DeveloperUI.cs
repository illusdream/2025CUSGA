using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Upper, 1, true, EAssetLoadMode.Resources, "Prefab/Shili/DeveloperUI")]
public class DeveloperUI : UIPanel
{
    [AutoUIElement("Panel/Button")]
    private Button backButton;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        backButton.onClick.AddListener(base.Close);
    }
}
