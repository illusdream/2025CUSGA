using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Normal, 3, true, EAssetLoadMode.Resources, "Prefab/Shili/CustomPlayer1Set")]
public class CustomPlayer1SetUI : UIPanel
{
    public bool isPlayerOne;
    private bool ilsBool;
    private Dictionary<RectTransform, bool> _panelLockStates;
    [AutoUIElement("Panel")]
    private GameObject panel;
    [AutoUIElement("Panel/PlayerText")]
    public Text playerText;
    [AutoUIElement("Panel/Back")]
    private Button backButton;
    [AutoUIElement("Panel/Save")]
    private Button saveButton;
    [AutoUIElement("Panel/GameObject/Health/HealthInput/Text")]
    private Text healthText;
    [AutoUIElement("Panel/GameObject/Energy/EnergyInput/Text")]
    private Text energyText;
    [AutoUIElement("Panel/GameObject/Cube/CubeInput/Text")]
    private Text cubeText;
    [AutoUIElement("Panel/GameObject/Prop/PropButton")]
    private Button propButton;
    //用于更新携带道具的列表
    public List<int> propChoiceButtonSet1;
    public List<int> propChoiceButtonSet2;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        backButton.onClick.AddListener(Close);
        saveButton.onClick.AddListener(OnSave);
        propButton.onClick.AddListener(() => { UIManager.Instance.GetUIPanel<PropsPoolUI>().Open(); });
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
    public static bool DeepCompare(CustomPlayer objA, CustomPlayer objB)
    {
        if(objA.id == objB.id&&objA.health == objB.health&&objA.energy == objB.energy&&objA.cude == objB.cude)
        {
            return true;
        }
        return false;
    }
    public override void Close()
    {

        if (!ilsBool)
        {
            ilsBool = true;
            base.Close();
            return;
        }
        if (healthText.text == "" || energyText.text == "" || cubeText.text == "")
        {
            if (_panelLockStates.ContainsKey(panel.GetComponent<RectTransform>()) && _panelLockStates[panel.GetComponent<RectTransform>()])
            {
                return;
            }
            Shili_DOTweenManager.Instance.PlayPanelExit(panel.GetComponent<RectTransform>(), UIPanelCanvasGroup);
            return;
        }
        CustomPlayer customPlayer = null;
        bool isSame;
        if (isPlayerOne)
        {
            customPlayer = new CustomPlayer(1, int.Parse(healthText.text), int.Parse(energyText.text), int.Parse(cubeText.text), propChoiceButtonSet1);
            isSame = DeepCompare(customPlayer, shili_CustomUIManager.Instance.GetCustomPlayerlist()[0]);
        }
        else
        {
            customPlayer = new CustomPlayer(2, int.Parse(healthText.text), int.Parse(energyText.text), int.Parse(cubeText.text), propChoiceButtonSet2);
            isSame = DeepCompare(customPlayer, shili_CustomUIManager.Instance.GetCustomPlayerlist()[1]);
        }
        if (!isSame)
        {
            UIManager.Instance.GetUIPanel<NotSaveUI>().Open();
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
    public void OnSave()
    {
        if (healthText.text == "" || energyText.text == "" || cubeText.text == "")
        {
            UIManager.Instance.GetUIPanel< InputBugUI >().Open();
            return;
        }
        if(int.Parse(healthText.text)>999|| int.Parse(healthText.text) < 1 || int.Parse(energyText.text) > 999 || int.Parse(energyText.text) < 1 || int.Parse(cubeText.text) > 99 || int.Parse(cubeText.text) < 1)
        {
            UIManager.Instance.GetUIPanel<InputBugUI>().Open();
        }
        else
        {
            if(isPlayerOne)
            {
                shili_CustomUIManager.Instance.AddCustomPlayer(new CustomPlayer(1, int.Parse(healthText.text), int.Parse(energyText.text), int.Parse(cubeText.text), propChoiceButtonSet1));
                /*              Debug.Log("血量" + healthText.text);
                              Debug.Log("能量阈值" + energyText.text);
                              Debug.Log("方块数量" + cubeText.text);*/
                Close();
            }
            else
            {
                shili_CustomUIManager.Instance.AddCustomPlayer(new CustomPlayer(2, int.Parse(healthText.text), int.Parse(energyText.text), int.Parse(cubeText.text), propChoiceButtonSet2));
                Close();
            }

        }
    }
}
