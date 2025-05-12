using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Normal, 3, true, EAssetLoadMode.Resources, "Prefab/Shili/RandomEventUI")]
public class RandomEventUI : UIPanel
{
    [AutoUIElement()]
    private GameObject canvas;
    private bool ilsBool;
    private Dictionary<RectTransform, bool> _panelLockStates;
    [AutoUIElement("Panel")]
    private GameObject panel;
    [AutoUIElement("Panel/Back")]
    private Button backButton;
    [AutoUIElement("Panel/Save")]
    private Button saveButton;
    [AutoUIElement("Panel/GameObject/Two/Scroll View/Viewport/ChioceProp")]
    private GameObject chioceObject;//因为这里是简单的拷贝了道具类，所以很多名字有错误
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
        canvas.GetComponent<RandomEventlUICanvas>().SetOnEnable();
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
        List<RandomEventButtonSet> randomEventButtonSets = new List<RandomEventButtonSet>();
        for (int i = 0; i < chioceObject.transform.childCount; i++)
        {
            randomEventButtonSets.Add(chioceObject.transform.GetChild(i).GetComponent<RandomEventButtonSet>());
        }
        List<ERandomEventType> shiliList = shili_CustomUIManager.Instance.GetRandomEventButtonSet();
        if(CheakSame(randomEventButtonSets, shiliList))
        {
            if (_panelLockStates.ContainsKey(panel.GetComponent<RectTransform>()) && _panelLockStates[panel.GetComponent<RectTransform>()])
            {
                return;
            }
            canvas.GetComponent<RandomEventlUICanvas>().SetOnDisable();
            Shili_DOTweenManager.Instance.PlayPanelExit(panel.GetComponent<RectTransform>(), UIPanelCanvasGroup);
        }
        else
        {
            UIManager.Instance.GetUIPanel<NotSaveUIAboutRandomEvent>().Open();
        }
    }
    public void OnSave()
    {
        List<RandomEventButtonSet> randomEventButtonSets = new List<RandomEventButtonSet>();
        for(int i = 0;i< chioceObject.transform.childCount; i++)
        {
            randomEventButtonSets.Add(chioceObject.transform.GetChild(i).GetComponent<RandomEventButtonSet>());
        }
        shili_CustomUIManager.Instance.SetRandomEventButtonSets(randomEventButtonSets);
        Close();
    }
    private bool CheakSame(List<RandomEventButtonSet> randomEventButtonSets, List<ERandomEventType> shiliList)
    {
        if(randomEventButtonSets.Count!= shiliList.Count) return false;
        for(int i = 0;i< randomEventButtonSets.Count; i++)
        {
            if (randomEventButtonSets[i].id!= shiliList[i])
            {
                return false;
            }
        }
        return true;
    }
}
