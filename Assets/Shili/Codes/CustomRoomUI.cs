using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[UIPanelSetting(EUILayer.Normal, 2, true, EAssetLoadMode.Resources, "Prefab/Shili/CustomRoom")]
public class CustomRoomUI : UIPanel
{
    private bool isStart;
    private MainInputAction inputActions;
    private bool ilsBool;
    private Dictionary<RectTransform, bool> _panelLockStates;
    [AutoUIElement("Panel")]
    private GameObject panel;
    [AutoUIElement("Panel/Back")]
    private Button backButton;
    [AutoUIElement("Panel/GameObject/Player1")]
    private Button player1Button;
    [AutoUIElement("Panel/GameObject/Player2")]
    private Button player2Button;
    [AutoUIElement("Panel/GameObject/MapSet")]
    private Button mapSetButton;
    [AutoUIElement("Panel/GameObject/RandomEvent")]
    private Button randomEventUI;
    [AutoUIElement("Panel/StartGame")]
    private Button startGame;
    public override void InitUIPanel()
    {
        inputActions = InputManager.Instance.GetCurrentInputAction();
        base.InitUIPanel();
        backButton.onClick.AddListener(Close);
        startGame.onClick.AddListener(OnStart);
        randomEventUI.onClick.AddListener(() => { UIManager.Instance.GetUIPanel<RandomEventUI>().Open(); });
        mapSetButton.onClick.AddListener(() => { UIManager.Instance.GetUIPanel<MapSetUI>().Open(); });
        player1Button.onClick.AddListener(() => { UIManager.Instance.GetUIPanel<CustomPlayer1SetUI>().Open(); UIManager.Instance.GetUIPanel<CustomPlayer1SetUI>().playerText.text = "Player1 …Ë÷√"; UIManager.Instance.GetUIPanel<CustomPlayer1SetUI>().isPlayerOne = true; });
        player2Button.onClick.AddListener(() => { UIManager.Instance.GetUIPanel<CustomPlayer1SetUI>().Open(); UIManager.Instance.GetUIPanel<CustomPlayer1SetUI>().playerText.text = "Player2 …Ë÷√"; UIManager.Instance.GetUIPanel<CustomPlayer1SetUI>().isPlayerOne = false; });
        _panelLockStates = Shili_DOTweenManager.Instance._panelLockStates;
    }
    private void OnStart()
    {
        isStart = true;
        Close();
    }
    public override void Open()
    {
        inputActions.Disable();
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
            if (isStart)
            {
                isStart = false;
                shili_CustomUIManager.Instance.OnStartGame();
                UIManager.Instance.GetUIPanel<ChoiceModeUI>().Close();
            }
            inputActions.Enable();
            Shili_DOTweenManager.Instance.PlayPanelExit(panel.GetComponent<RectTransform>(), UIPanelCanvasGroup);
        }

    }
}
