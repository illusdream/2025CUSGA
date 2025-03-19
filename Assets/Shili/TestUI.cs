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
        inputActions.GamePlay.TEst.started += On;
        inputActions.Enable();
    }
    private void On(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("’‚ «≤‚ ‘∞¥º¸");
    }
}
