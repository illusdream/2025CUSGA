using System;
using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Lower, 11, true, EAssetLoadMode.Resources, "Prefab/Shili/StopGame")]
public class StopGameUI : UIPanel
{
    [AutoUIElement("Panel/Continue")]
    private Button continueButton;
    [AutoUIElement("Panel/Setting")]
    private Button settingButton;
    [AutoUIElement("Panel/Resume")]
    private Button resumeButton;
    [AutoUIElement("Panel/Exit")]
    private Button exitButton;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        continueButton.onClick.AddListener(OnContinue);
        settingButton.onClick.AddListener(OnSetting);
        resumeButton.onClick.AddListener(OnResume);
        exitButton.onClick.AddListener(OnExit);

    }
    public override void Open()
    {
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }
    private void OnContinue()
    {
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToResumeGame,EventArgs.Empty);
        UIManager.Instance.GetUIPanel<StopGameUI>().Close();
        UIManager.Instance.GetUIPanel<InHouseUI>().Open();
    }
    private void OnSetting()
    {
        //UIManager.Instance.GetUIPanel<StopGameUI>().Close();
        UIManager.Instance.GetUIPanel<SettingUI>().Open();
    }
    private void OnResume()
    {
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToRestartGamePlay, EventArgs.Empty);
    }
    private void OnExit()
    {
        UIManager.Instance.GetUIPanel<StopGameUI>().Close();
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToSwitchToMainMenu, EventArgs.Empty);
        UIManager.Instance.GetUIPanel<InHouseUI>().Close();
        UIManager.Instance.GetUIPanel<MenuUI>().Open();
        //UIManager.Instance.GetUIPanel<OnOpenGameNextUI>().Close();
    }
}
