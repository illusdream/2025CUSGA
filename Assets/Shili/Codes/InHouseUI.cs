using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ilsFramework;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Lower, 10, true, EAssetLoadMode.Resources, "Prefab/Shili/InHouseUI")]
public class InHouseUI : UIPanel
{
    //���1��ʮ�ֻ�����Ӣ�����дע����
    [AutoUIElement("Panel1/GameObject1/Text")]
    private Text player1NameText;
    [AutoUIElement("Panel1/GameObject1/Image")]
    private Image player1Headshot;
    [AutoUIElement("Panel1/GameObject3")]
    private GameObject player1SkillSlotsObject;
    //���2
    [AutoUIElement("Panel2/GameObject1/Text")]
    private Text player2NameText;
    [AutoUIElement("Panel2/GameObject1/Image")]
    private Image player2Headshot;

    [AutoUIElement("Panel2/GameObject3")]
    private GameObject player2SkillSlotsObject;
    
    public override void InitUIPanel()
    {
        base.InitUIPanel();
    }
    public override void Open()
    {
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }
    public override void Update()
    {
        base.Update();
        
    }
}
