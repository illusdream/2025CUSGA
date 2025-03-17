using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Bottom, 0, true, EAssetLoadMode.Resources, "Prefab/Shili/Menu")]
public class MenuUI : UIPanel
{
    [AutoUIElement("Panel/GameObject/StartGame")]
    private Button startGameButton;
    [AutoUIElement("Panel/GameObject/Setting")]
    private Button settingButton;
    [AutoUIElement("Panel/GameObject/AboutOur")]
    private Button aboutOurButton;
    [AutoUIElement("Panel/GameObject/ExitGame")]
    private Button exitGameButton;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        // startGameButton.onClick.AddListener(OnOpenGame);
        // settingButton.onClick.AddListener(OnOpenSetting);
        // aboutOurButton.onClick.AddListener(OnOpenAboutOur);
        // exitGameButton.onClick.AddListener(OnOpenExitGame);
    }
    public override void Open()
    {
        base.Open();
        Debug.Log("��MenuUI");
    }
    public override void Close()
    {
        base.Close();
        Debug.Log("�ر�MenuUI");
    }
    private void OnOpenGame()
    {
        Debug.Log("��ʼ��Ϸ");
    }
    private void OnOpenSetting()
    {
        Debug.Log("������");
    }
    private void OnOpenAboutOur()
    {
        Debug.Log("��������");
    }
    private void OnOpenExitGame()
    {
        Debug.Log("�˳���Ϸ");
        Application.Quit();
    }
}
