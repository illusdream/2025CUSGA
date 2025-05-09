using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Normal, 1, true, EAssetLoadMode.Resources, "Prefab/Shili/ChoiceMode")]
public class ChoiceModeUI : UIPanel
{
    private Dictionary<RectTransform, bool> _panelLockStates;
    private bool ilsBool;
    [AutoUIElement("Panel/GameObject")]
    private GameObject panel;
    [AutoUIElement("Panel/GameObject/NormalMode")]
    private Button normalModeButton;
    [AutoUIElement("Panel/GameObject/CustomMode")]
    private Button customModeButton;
    [AutoUIElement("Panel/Back")]
    private Button backButton;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        normalModeButton.onClick.AddListener(OnNormalModeGame);
        customModeButton.onClick.AddListener(OnCustomModeGame);
        backButton.onClick.AddListener(Close);
        _panelLockStates = Shili_DOTweenManager.Instance._panelLockStates;
    }
    private void OnNormalModeGame()
    {
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderStartGame, EventArgs.Empty);
        Close();
    }
    private void OnCustomModeGame()
    {
        Debug.Log("进入自定义");
    }
    public override void Open()
    {
        if (_panelLockStates.ContainsKey(panel.GetComponent<RectTransform>()) && _panelLockStates[panel.GetComponent<RectTransform>()])
        {
            return;
        }

        base.Open();
        Shili_DOTweenManager.Instance.PlayPanelEnter(panel.GetComponent<RectTransform>(), UIPanelCanvasGroup);
    }
    public override void Close()
    {
        if (!ilsBool)
        {
            ilsBool = true;
            base.Close();
        }
        else
        {
            if (_panelLockStates.ContainsKey(panel.GetComponent<RectTransform>()) && _panelLockStates[panel.GetComponent<RectTransform>()])
            {
                return;
            }

            Shili_DOTweenManager.Instance.PlayPanelExit(panel.GetComponent<RectTransform>(), UIPanelCanvasGroup);
        }

    }
}
