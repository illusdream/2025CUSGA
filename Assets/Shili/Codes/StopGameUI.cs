using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Lower, 11, true, EAssetLoadMode.Resources, "Prefab/Shili/StopGame")]
public class StopGameUI : UIPanel
{
    [AutoUIElement("Panel/Continue")]
    private Button continueButton;
    [AutoUIElement("Panel/Setting")]
    private Button settingButton;
    [AutoUIElement("Panel/Resume")]
    private Button resumeButton;
    [AutoUIElement("Panel/Exit")]
    private Button exitButton;
    public override void InitUIPanel()
    {
        base.InitUIPanel();
        continueButton.onClick.AddListener(OnContinue);
        settingButton.onClick.AddListener(OnSetting);
        resumeButton.onClick.AddListener(OnResume);
        exitButton.onClick.AddListener(OnExit);

    }
    public override void Open()
    {
        base.Open();
        Time.timeScale = 0f;
    }
    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
    }
    private void OnContinue()
    {
        UIManager.Instance.GetUIPanel<StopGameUI>().Close();
    }
    private void OnSetting()
    {
        UIManager.Instance.GetUIPanel<StopGameUI>().Close();
        UIManager.Instance.GetUIPanel<SettingUI>().Open();
    }
    private void OnResume()
    {
        UIManager.Instance.GetUIPanel<StopGameUI>().Close();
        Debug.Log("重新开始");
    }
    private void OnExit()
    {
        UIManager.Instance.GetUIPanel<StopGameUI>().Close();
        Debug.Log("回到主页面");
    }
}
