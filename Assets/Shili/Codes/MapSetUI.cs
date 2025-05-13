using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Normal, 3, true, EAssetLoadMode.Resources, "Prefab/Shili/MapSetUI")]
public class MapSetUI : UIPanel
{
    private bool ilsBool;
    private Dictionary<RectTransform, bool> _panelLockStates;
    [AutoUIElement("Panel")]
    private GameObject panel;
    [AutoUIElement("Panel/Back")]
    private Button backButton;
    [AutoUIElement("Panel/Save")]
    private Button saveButton;
    [AutoUIElement("Panel/GameObject/PlayerCubeHealth/PlayerCubeHealthInput/Text")]
    private Text playerCubeHealthText;
    [AutoUIElement("Panel/GameObject/NeutralCubeHealth/NeutralCubeHealthInput/Text")]
    private Text neutralCubeHealthText;
    [AutoUIElement("Panel/GameObject/CubeTime/CubeTimeInput/Text")]
    private Text cubeTimeText;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        backButton.onClick.AddListener(()=>
        {
            Close(); AudioUtils.PlayUIClick();
        });
        saveButton.onClick.AddListener(OnSave);
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
            return;
        }
        if (playerCubeHealthText.text == "" || neutralCubeHealthText.text == "" || cubeTimeText.text == "")
        {
            if (_panelLockStates.ContainsKey(panel.GetComponent<RectTransform>()) && _panelLockStates[panel.GetComponent<RectTransform>()])
            {
                return;
            }
            Shili_DOTweenManager.Instance.PlayPanelExit(panel.GetComponent<RectTransform>(), UIPanelCanvasGroup);
            return;
        }
        MapSet mapSet = shili_CustomUIManager.Instance.GetMapSet();
        if(mapSet.playerCubeHealth == int.Parse(playerCubeHealthText.text)&&mapSet.neutralCubeHealth== int.Parse(neutralCubeHealthText.text)&&mapSet.cubeTime== int.Parse(cubeTimeText.text))
        {
            if (_panelLockStates.ContainsKey(panel.GetComponent<RectTransform>()) && _panelLockStates[panel.GetComponent<RectTransform>()])
            {
                return;
            }
            Shili_DOTweenManager.Instance.PlayPanelExit(panel.GetComponent<RectTransform>(), UIPanelCanvasGroup);
        }
        else
        {
           UIManager.Instance.GetUIPanel<NotSaveUIAboutMapSet>().Open();
        }
    }
    public void OnSave()
    {
        AudioUtils.PlayUIClick();
        if (playerCubeHealthText.text == "" || neutralCubeHealthText.text == "" || cubeTimeText.text == "")
        {
            UIManager.Instance.GetUIPanel<InputBugUI>().Open();
            return;
        }
        if (int.Parse(playerCubeHealthText.text) > 999 || int.Parse(playerCubeHealthText.text) < 1 || int.Parse(neutralCubeHealthText.text) > 999 || int.Parse(neutralCubeHealthText.text) < 1 || int.Parse(cubeTimeText.text) > 99 || int.Parse(cubeTimeText.text) < 1)
        {
            UIManager.Instance.GetUIPanel<InputBugUI>().Open();
        }
        else
        {
            shili_CustomUIManager.Instance.SetMapSet(int.Parse(playerCubeHealthText.text), int.Parse(neutralCubeHealthText.text), int.Parse(cubeTimeText.text));
            Debug.Log("����");
            Close();
        }
    }
}
