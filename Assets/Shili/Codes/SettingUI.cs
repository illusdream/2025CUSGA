using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Upper, 1, true, EAssetLoadMode.Resources, "Prefab/Shili/SettingUI")]
public class SettingUI : UIPanel
{
    private Dictionary<RectTransform, bool> _panelLockStates;
    public bool ilsBool;
    [AutoUIElement("Panel")]
    private GameObject panel;
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
    [AutoUIElement("Panel/KeystrokeSetting")]
    private GameObject keystrokeSettingObject;
    [AutoUIElement("Panel/KeystrokeSetting/Reset")]
    private Button ResetButton;
    //����
    private bool isPlayMusic;
    public override void InitUIPanel()
    {
        
        isPlayMusic = true;
        base.InitUIPanel();
        backButton.onClick.AddListener(OnResumeGame);
        gameSettingButton.GetComponent<Text>().fontStyle = FontStyle.Bold;
        gameSettingButton.GetComponent<Button>().onClick.AddListener(OnGameSetting);
        keystrokeSettingButton.GetComponent<Button>().onClick.AddListener(OnKeystrokeSetting);
        MusicSwitch.GetComponent<Button>().onClick.AddListener(OnSoundSwitch);
        mainMusicSlider.onValueChanged.AddListener(OnMainMusicSlider);
        backgroundMusicSlider.onValueChanged.AddListener(OnBackgroundMusicSlider);
        soundSlider.onValueChanged.AddListener(OnSoundSlider);
        showDropdown.onValueChanged.AddListener(OnShowChange);
        resDropdown.onValueChanged.AddListener(OnResChange);
        ResetButton.onClick.AddListener(OnReset);
        _panelLockStates = Shili_DOTweenManager.Instance._panelLockStates;
    }

    private void OnResumeGame()
    {
        //GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToResumeGame,EventArgs.Empty);
        Close();
    }
    private void OnReset()
    {
        InputManager.Instance.ResetAllBindings();
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.ResetKey, EventArgs.Empty);
        Debug.Log("����");
    }
    private void OnGameSetting()
    {
        gameSettingButton.GetComponent<Text>().fontStyle = FontStyle.Bold;
        keystrokeSettingButton.GetComponent <Text>().fontStyle = FontStyle.Normal;
        gameSettingObject.SetActive(true);
        keystrokeSettingObject.SetActive(false);
    }
    private void OnKeystrokeSetting()
    {
        gameSettingButton.GetComponent<Text>().fontStyle = FontStyle.Normal;
        keystrokeSettingButton.GetComponent<Text>().fontStyle = FontStyle.Bold;
        gameSettingObject.SetActive(false);
        keystrokeSettingObject.SetActive(true);
    }
    private void OnSoundSwitch()
    {
        isPlayMusic = !isPlayMusic;
        if (isPlayMusic)
        {
            MusicSwitch.GetComponent<Image>().color = Color.white;
            
            Debug.Log("��������");
        }
        else
        {
            MusicSwitch.GetComponent<Image>().color = Color.red;
            AudioManager.Instance.StopAll();
            Debug.Log("�ر�����");
        }
    }
    private void OnMainMusicSlider(float a)
    {
        AudioManager.Instance.SetMainVolume(a);
        Debug.Log("������"+a);
    }
    private void OnBackgroundMusicSlider(float a)
    {
        AudioManager.Instance.SetChannelVolume(AudioChannelName.BGM,a);
        Debug.Log("��������" + a);
    }
    private void OnSoundSlider(float a)
    {
        AudioManager.Instance.SetChannelVolume(AudioChannelName.Sound,a);
        Debug.Log("��Ч" + a);
    }
    private void OnShowChange(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("quan ping");
                ScreenManager.Instance.SetIsFullScreen(true);
                break;
            case 1:
                Debug.Log("chuang kou hua");
                ScreenManager.Instance.SetIsFullScreen(false);
                break;
        }
    }
    private void OnResChange(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("1920*1080");
                ScreenManager.Instance.SetCurrentScreenSize(new Vector2Int(1920, 1080));
                break;
            case 1:
                Debug.Log("1280*720");
                ScreenManager.Instance.SetCurrentScreenSize(new Vector2Int(1280, 720));
                break;
        }
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
