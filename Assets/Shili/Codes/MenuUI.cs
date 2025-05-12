using System;
using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;
using DG.Tweening;
[UIPanelSetting(EUILayer.Normal, 0, true, EAssetLoadMode.Resources, "Prefab/Shili/Menu")]
public class MenuUI : UIPanel
{
    [AutoUIElement("Panel")]
    private GameObject panel;
    [AutoUIElement("Panel/GameObject")]
    private GameObject m_GameObject;
    [AutoUIElement("Panel/GameObject/StartGame")]
    private Button startGameButton;
    [AutoUIElement("Panel/GameObject/Setting")]
    private Button settingButton;
    [AutoUIElement("Panel/GameObject/AboutOur")]
    private Button aboutOurButton;
    [AutoUIElement("Panel/GameObject/ExitGame")]
    private Button exitGameButton;
    [AutoUIElement("Panel/GameObject/Guidelines")]
    private Button guidelinesButton;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        startGameButton.onClick.AddListener(OnSatrtGame);
        settingButton.onClick.AddListener(OnOpenSetting);
        aboutOurButton.onClick.AddListener(OnOpenAboutOur);
        exitGameButton.onClick.AddListener(OnOpenExitGame);
        guidelinesButton.onClick.AddListener(OnGuidelinesScene);
    }
    private void OnSatrtGame()
    {
        UIManager.Instance.GetUIPanel<ChoiceModeUI>().Open();
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
    private void OnGuidelinesScene()
    {
        
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToGuidelinesScene, EventArgs.Empty);
    }
    public override void Open()
    {
        base.Open();
        GameManager.Instance.SetDefaultConfigs();
        Shili_DOTweenManager.Instance.PlayPanelEnter(m_GameObject.GetComponent<RectTransform>(), UIPanelCanvasGroup);
    }
}


