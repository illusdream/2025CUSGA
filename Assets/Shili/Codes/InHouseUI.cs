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
        Debug.Log("��InHouseUI");
    }
    public override void Close()
    {
        base.Close();
        Debug.Log("�ر�InHouseUI");
    }
    public override void Update()
    {
        base.Update();
        //player1HealthImage.fillAmount = 0��1;
        //player1EnergyImage.fillAmount = 0��1;
        //player2HealthImage.fillAmount = 0��1;
        //player2EnergyImage.fillAmount = 0��1;
        
    }
}
