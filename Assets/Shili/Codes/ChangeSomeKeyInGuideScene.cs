using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using Tiles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChangeSomeKeyInGuideScene : MonoBehaviour
{
    [Header("索引与UI")]
    public int index;
    public Text text;
    public string[] textStr;
    public RectTransform panel;
    public Vector2[] widthAndHeight;
    public Vector2[] posXAndPosY;
    [Header("阶段一：生成的两个块的位置和高光")]
    public Vector2Int vector21;
    public Vector2Int vector22;
    private MainInputAction inputActions;
    public GameObject baikuang2; 
    [Header("阶段二：生成的两个不可破坏块的位置")]
    public Vector2Int vector01;
    public Vector2Int vector02;
    [Header("阶段三：生成高光")]
    public GameObject baikuang;
    [Header("阶段四")]
    public GameObject baikuang3;
    public Vector2Int[] vector2Ints;
    [Header("阶段五")]
    [Header("阶段六")]
    [Header("阶段七")]
    [Header("阶段八")]
    public GuidelinesSceneInit guidelinesSceneInit;
    [Header("阶段九")]
    public GameObject panel1;
    [Header("阶段十（后）")]
    public GameObject textEnter;
    [Header("阶段十一")]
    public GameObject panel2;
    private void Awake()
    {
        inputActions = InputManager.Instance.GetCurrentInputAction();
    }
    private void OnEnable()
    {
        Shili_DOTweenManager.Instance.FadePanel(GetComponent<CanvasGroup>(),GetComponent<RectTransform>());
        inputActions.GamePlay.Enter.started += OnEnterDown;
        inputActions.GamePlay.Player1ChangeProp.canceled += InputKeyI;
        inputActions.GamePlay.Player1UseProp.canceled += OnEnterDown;
        inputActions.GamePlay.Player1UseProp.canceled += InputKeyL;
        EnterTheStage();
        UpOnEnable();
    }
    private void Start()
    {
        inputActions.GamePlay.Enter.Enable();
    }
    private void OnDisable()
    {
        inputActions.GamePlay.Enter.started -= OnEnterDown;
        inputActions.GamePlay.Player1ChangeProp.canceled -= InputKeyI;
        inputActions.GamePlay.Player1UseProp.canceled -= OnEnterDown;
        inputActions.GamePlay.Player1UseProp.canceled -= InputKeyL;
    }
    private void OnEnterDown(InputAction.CallbackContext callback)
    {
        ExitTheStage();
        if (index == 2||index==5|| index == 6|| index == 7||index == 8||index == 9|| index == 10 || index == 11 || index == 13||index == 14)
        {
            EnterTheStage();
            UpOnEnable();
            return;
        }
        if(index == 15)
        {
            /*Debug.Log("start");
            TileManager.Instance.StartFillRandomRange();
            TileManager.Instance.GenerateTiles();
            guidelinesSceneInit.ResomeEnergy();*/
            GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToSwitchToMainMenu, EventArgs.Empty);
        }
        gameObject.SetActive(false);
    }
    private void UpOnEnable()
    {
        text.text = textStr[index];
        panel.sizeDelta = widthAndHeight[index];
        panel.anchoredPosition = posXAndPosY[index];
        switch (index)
        {
            case 0:
                TileManager.Instance.SetTile(false ? typeof(AirTile) : typeof(CommonTile), vector21, EntityID.Empty);
                TileManager.Instance.SetTile(false ? typeof(AirTile) : typeof(CommonTile), vector22, EntityID.Empty);
                baikuang2.SetActive(true);
                break;
            case 1:
                baikuang2.SetActive(false);
                TileManager.Instance.SetTile(false ? typeof(AirTile) : typeof(SolidTile), vector01, EntityID.Empty);
                TileManager.Instance.SetTile(false ? typeof(AirTile) : typeof(SolidTile), vector02, EntityID.Empty);
                break;
            case 2:
                baikuang.SetActive(true);
                break;
            case 3:
                baikuang.SetActive(false);
                baikuang3.SetActive(true);
                for (int i = 0;i< vector2Ints.Length; i++)
                {
                    TileManager.Instance.SetTile(false ? typeof(AirTile) : typeof(SolidTile), vector2Ints[i], EntityID.Empty);
                }
                break;
            case 4:
                baikuang3.SetActive(false);
                break;
            case 5:
                break;//只更新了UI
            case 6:
                break;//只更新了UI
            case 7:
                guidelinesSceneInit.GivePlayerProp();
                break;
            case 8:
                //还有一个遮罩在玩家道具使用次数UI上没做
                panel1.SetActive(true);
                break;
            case 9:
                break;//只更新了UI
            case 10:
                textEnter.SetActive(false);
                break;
            case 11:
                //需要有使用道具的信号触发
                //还有一个遮罩在玩家护盾UI上没做
                panel2.SetActive(true);
                break;
            case 12:
                //触发陨石雨随机事件，并且有一块陨石砸到玩家身上，而后再进入下一阶段
                break;
            case 13:
                break;
            case 14:
                break;
        }
        index++;
    }
    /// <summary>
    /// index为阶段index+1前（进入新阶段前要禁用/启用的按键）
    /// </summary>
    private void EnterTheStage()
    {
        switch (index)
        {
            case 0:
                break;
            case 1:
                inputActions.GamePlay.Player1Move.Disable();
                inputActions.GamePlay.Player1BreakTile.Disable();
                break;
            case 3:
                inputActions.GamePlay.Player1Move.Disable();
                inputActions.GamePlay.Player1BreakTile.Disable();
                inputActions.GamePlay.Player1PlaceTile.Disable();
                break;
            case 4:
                inputActions.GamePlay.Player1Move.Disable();
                inputActions.GamePlay.Player1BreakTile.Disable();
                inputActions.GamePlay.Player1PlaceTile.Disable();
                break;
            case 8:
                
                break;
            case 10:
                
                break;
            case 11:
                textEnter.SetActive(true);
                inputActions.GamePlay.Enter.Enable();
                break;
            case 12:
                break;
        }
    }
    /// <summary>
    /// index为阶段index后（进入新阶段后要禁用/启用的按键）
    /// </summary>
    private void ExitTheStage()
    {
        switch (index)
        {
            case 1:
                inputActions.GamePlay.Player1Move.Enable();
                inputActions.GamePlay.Player1BreakTile.Enable();
                break;
            case 3:
                inputActions.GamePlay.Player1Move.Enable();
                inputActions.GamePlay.Player1BreakTile.Enable();
                inputActions.GamePlay.Player1PlaceTile.Enable();
                break;
            case 4:
                inputActions.GamePlay.Player1Move.Enable();
                inputActions.GamePlay.Player1BreakTile.Enable();
                inputActions.GamePlay.Player1PlaceTile.Enable();
                break;
            case 9:
                panel1.SetActive(false);
                break;
            case 10:
                inputActions.GamePlay.Enter.Disable();
                inputActions.GamePlay.Player1ChangeProp.Enable();
                break;
            case 11:
                break;
            case 12:
                guidelinesSceneInit.Rain();
                panel2.SetActive(false);
                break;
        }
    }
    private void InputKeyI(InputAction.CallbackContext callbackContext)
    {
        inputActions.GamePlay.Player1UseProp.Enable();
        inputActions.GamePlay.Player1ChangeProp.Disable();
    }
    private void InputKeyL(InputAction.CallbackContext callbackContext)
    {
        inputActions.GamePlay.Player1UseProp.Disable();
    }
}
