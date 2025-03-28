using System;
using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Normal, 0, true, EAssetLoadMode.Resources, "Prefab/Shili/Menu")]
public class MenuUI : UIPanel
{
    [AutoUIElement("Panel/GameObject/StartGame")]
    private Button startGameButton;
    [AutoUIElement("Panel/GameObject/Setting")]
    private Button settingButton;
    [AutoUIElement("Panel/GameObject/AboutOur")]
    private Button aboutOurButton;
    [AutoUIElement("Panel/GameObject/ExitGame")]
    private Button exitGameButton;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        startGameButton.onClick.AddListener(OnSatrtGame);
        settingButton.onClick.AddListener(OnOpenSetting);
        aboutOurButton.onClick.AddListener(OnOpenAboutOur);
        exitGameButton.onClick.AddListener(OnOpenExitGame);
    }
    private void OnSatrtGame()
    {
        GlobalEventCenter.Instance.BoardCastMessage(GlobalEventSets.OrderStartGame,EventArgs.Empty);
    }
    private void OnOpenSetting()
    {
        UIManager.Instance.GetUIPanel<SettingUI>().Open();
    }
    private void OnOpenAboutOur()
    {
        UIManager.Instance.GetUIPanel<DeveloperUI>().Open();
    }
    private void OnOpenExitGame()
    {
        Application.Quit();
    }
}
