using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Normal, 6, true, EAssetLoadMode.Resources, "Prefab/Shili/Message")]
public class MessageUI : UIPanel
{
    private Dictionary<RectTransform, bool> _panelLockStates;
    private bool ilsBool;
    [AutoUIElement()]
    private GameObject canvas;
    [AutoUIElement("Panel")]
    private GameObject panel;
    [AutoUIElement("Panel/RectMask/Text")]
    public Text text;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        _panelLockStates = Shili_DOTweenManager.Instance._panelLockStates;
    }
    public override void Open()
    {
        if (_panelLockStates.ContainsKey(panel.GetComponent<RectTransform>()) && _panelLockStates[panel.GetComponent<RectTransform>()])
        {
            return;
        }
        canvas.GetComponent<MessageUISet>().SetOnEnable();
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