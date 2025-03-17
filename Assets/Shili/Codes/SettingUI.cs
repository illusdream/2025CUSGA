using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Bottom, 1, true, EAssetLoadMode.Resources, "Prefab/Shili/SettingUI")]
public class SettingUI : UIPanel
{
    [AutoUIElement("Panel/Back")]
    private Button backButton;
    [AutoUIElement("Panel/GameObject/GameSetting")]
    private GameObject gameSettingButton;
    [AutoUIElement("Panel/GameObject/KeystrokeSetting")]
    private GameObject keystrokeSettingButton;
    [AutoUIElement("Panel/GameSetting/GameObject/GameObject/Image")]
    private GameObject MusicSwitch;
    [AutoUIElement("Panel/GameSetting/GameObject/MainMusic/Slider")]
    private Slider mainMusicSlider;
    [AutoUIElement("Panel/GameSetting/GameObject/BackgroundMusic/Slider")]
    private Slider backgroundMusicSlider;
    [AutoUIElement("Panel/GameSetting/GameObject/Sound/Slider")]
    private Slider soundSlider;
    [AutoUIElement("Panel/GameSetting/PictureObj/GameObjectShow/DropdownShow")]
    private TMP_Dropdown showDropdown;
    [AutoUIElement("Panel/GameSetting/PictureObj/GameObjectRes/DropdownRes")]
    private TMP_Dropdown resDropdown;
    [AutoUIElement("Panel/GameSetting")]
    private GameObject gameSettingObject;
    //参数
    private bool isPlayMusic;
    public override void InitUIPanel()
    {
        isPlayMusic = true;
        base.InitUIPanel();
        backButton.onClick.AddListener(base.Close);
        gameSettingButton.GetComponent<Text>().fontStyle = FontStyle.Bold;
        gameSettingButton.GetComponent<Button>().onClick.AddListener(OnGameSetting);
        keystrokeSettingButton.GetComponent<Button>().onClick.AddListener(OnKeystrokeSetting);
        MusicSwitch.GetComponent<Button>().onClick.AddListener(OnSoundSwitch);
        mainMusicSlider.onValueChanged.AddListener(OnMainMusicSlider);
        backgroundMusicSlider.onValueChanged.AddListener(OnBackgroundMusicSlider);
        soundSlider.onValueChanged.AddListener(OnSoundSlider);
        showDropdown.onValueChanged.AddListener(OnShowChange);
        resDropdown.onValueChanged.AddListener(OnResChange);
    }
    private void OnGameSetting()
    {
        gameSettingButton.GetComponent<Text>().fontStyle = FontStyle.Bold;
        keystrokeSettingButton.GetComponent <Text>().fontStyle = FontStyle.Normal;
        gameSettingObject.SetActive(true);
    }
    private void OnKeystrokeSetting()
    {
        gameSettingButton.GetComponent<Text>().fontStyle = FontStyle.Normal;
        keystrokeSettingButton.GetComponent<Text>().fontStyle = FontStyle.Bold;
        gameSettingObject.SetActive(false);
    }
    private void OnSoundSwitch()
    {
        isPlayMusic = !isPlayMusic;
        if (isPlayMusic)
        {
            MusicSwitch.GetComponent<Image>().color = Color.white;
            Debug.Log("播放音乐");
        }
        else
        {
            MusicSwitch.GetComponent<Image>().color = Color.red;
            Debug.Log("关闭音乐");
        }
    }
    private void OnMainMusicSlider(float a)
    {
        Debug.Log("主音量"+a);
    }
    private void OnBackgroundMusicSlider(float a)
    {
        Debug.Log("背景音量" + a);
    }
    private void OnSoundSlider(float a)
    {
        Debug.Log("音效" + a);
    }
    private void OnShowChange(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("全屏");
                break;
            case 1:
                Debug.Log("窗口化");
                break;
        }
    }
    private void OnResChange(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("1920*1080");
                break;
            case 1:
                Debug.Log("1280*720");
                break;
        }
    }
}
