using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Normal, 4, true, EAssetLoadMode.Resources, "Prefab/Shili/PropsPoolUI")]
public class PropsPoolUI : UIPanel
{
    private Dictionary<RectTransform, bool> _panelLockStates;
    private bool ilsBool;
    [AutoUIElement("Panel")]
    private GameObject panel;
    [AutoUIElement("Panel/Back")]
    private Button backButton;
    [AutoUIElement()]
    private GameObject canvas;
    [AutoUIElement("Panel/Save")]
    private Button saveButton;
/*    [AutoUIElement("Panel/GameObject/One/Scroll View/Viewport/PropPool")]
    private GameObject propPoolGameObject;*/
    [AutoUIElement("Panel/GameObject/Two/Scroll View/Viewport/ChioceProp")]
    private GameObject chiocePropGameObject;
    [AutoUIElement("Panel/PlayerText")]
    public Text playerText;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        backButton.onClick.AddListener(Close);
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
        canvas.GetComponent<PropsPoolUICanvas>().SetOnEnable();
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
        List<PropChoiceButtonSet> p = new List<PropChoiceButtonSet>();
        for (int i = 0; i < chiocePropGameObject.transform.childCount; i++)
        {
            p.Add(chiocePropGameObject.transform.GetChild(i).GetComponent<PropChoiceButtonSet>());
        }
        bool isSave=false;
        if (UIManager.Instance.GetUIPanel<CustomPlayer1SetUI>().isPlayerOne)
        {
            if(shili_CustomUIManager.Instance.isSame1(p))
            {
                isSave = true;
            }
        }
        else
        {
            if (shili_CustomUIManager.Instance.isSame2(p))
            {
                isSave = true;
            }
        }
        if (!isSave)
        {
            //因为目前是依赖于实例来判断是否相等来判断是否保存，后续应换成ID的标识符，因为每次打开都是新的实例化，所以在每次重新打开时均会显示未保存
            UIManager.Instance.GetUIPanel<NotSaveUIAboutProp>().Open();
            return;
        }
        if (_panelLockStates.ContainsKey(panel.GetComponent<RectTransform>()) && _panelLockStates[panel.GetComponent<RectTransform>()])
        {
            return;
        }
        isSave = false;
        canvas.GetComponent<PropsPoolUICanvas>().SetOnDisable();
        Shili_DOTweenManager.Instance.PlayPanelExit(panel.GetComponent<RectTransform>(), UIPanelCanvasGroup);


    }
    public void OnSave()
    {
        List< PropChoiceButtonSet > p = new List< PropChoiceButtonSet >();
        if (chiocePropGameObject.transform.childCount == 0)
        {
            UIManager.Instance.GetUIPanel< FollowBugUI >().Open();
            return;
        }
        for(int i = 0;i< chiocePropGameObject.transform.childCount; i++)
        {
            p.Add(chiocePropGameObject.transform.GetChild(i).GetComponent<PropChoiceButtonSet>());
        }
        List<int> p1 = new List< int >();
        for(int i = 0; i < p.Count; i++)
        {
            p1.Add(p[i].id);
        }
        if(UIManager.Instance.GetUIPanel<CustomPlayer1SetUI>().isPlayerOne)
        {
            shili_CustomUIManager.Instance.GetCustomPlayerlist()[0].propChoiceButtonSet = p1;
        }
        else
        {
            shili_CustomUIManager.Instance.GetCustomPlayerlist()[1].propChoiceButtonSet = p1;
        }
        Close();
    }
}