using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestUI : MonoBehaviour
{
    private MainInputAction inputActions;
    private void Awake()
    {
        inputActions = InputManager.Instance.GetCurrentInputAction();
        UIManager.Instance.GetUIPanel<MenuUI>().Open();
    }
    private void OnEnable()
    {
        //不用enable，Manager里已经处理了
        //inputActions.GamePlay.TEst.started += On;
        //inputActions.Enable();
    }
    private void On(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log("���ǲ��԰���");
    }
}
