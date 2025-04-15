using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[UIPanelSetting(EUILayer.Upper, 2, true, EAssetLoadMode.Resources, "Prefab/Shili/GameOver")]
public class GameOverUI : UIPanel
{
    [AutoUIElement("Resome")]
    private Button resumeButton;
    [AutoUIElement("BackMenu")]
    private Button exitButton;
    public override void InitUIPanel()
    {
        resumeButton.onClick.AddListener(this.OnResume);
        exitButton.onClick.AddListener(this.OnExit);
        base.InitUIPanel();
    }
    private void OnResume()
    {
        UIManager.Instance.GetUIPanel<GameOverUI>().Close();
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToRestartGamePlay, EventArgs.Empty);
    }
    private void OnExit()
    {
        UIManager.Instance.GetUIPanel<GameOverUI>().Close();
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToSwitchToMainMenu, EventArgs.Empty);
        UIManager.Instance.GetUIPanel<InHouseUI>().Close();
        UIManager.Instance.GetUIPanel<MenuUI>().Open();
        //UIManager.Instance.GetUIPanel<OnOpenGameNextUI>().Close();
    }
}
