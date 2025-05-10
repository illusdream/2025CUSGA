using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Normal, 6, true, EAssetLoadMode.Resources, "Prefab/Shili/NotSaveUI")]
public class NotSaveUI : UIPanel
{
    private bool save;
    private Dictionary<RectTransform, bool> _panelLockStates;
    private bool ilsBool;
    [AutoUIElement("Panel")]
    private GameObject panel;
    [AutoUIElement("Panel/GameObject/Back")]
    private Button backButton;
    [AutoUIElement("Panel/GameObject/SaveAndBack")]
    private Button saveAndBackButton;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        backButton.onClick.AddListener(OnClose);
        saveAndBackButton.onClick.AddListener(OnSaveAndBackButton);
        _panelLockStates = Shili_DOTweenManager.Instance._panelLockStates;
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
            if(save) UIManager.Instance.GetUIPanel<CustomPlayer1SetUI>().OnSave();
            Shili_DOTweenManager.Instance.PlayPanelExit(panel.GetComponent<RectTransform>(), UIPanelCanvasGroup);
        }

    }
    private void OnSaveAndBackButton()
    {
        save = true;
        Close();
    }
    private void OnClose()
    {
        save = false;
        Close();
    }
}