using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ChangeKeyCodes : MonoBehaviour
{
    private MainInputAction inputActions;
    private GameObject playerKey;
    private Keyboard keyboard;
    private InputAction inputAction;
    public string actionName;
    private void Awake()
    {

        playerKey = gameObject;
        inputActions = InputManager.Instance.GetCurrentInputAction();
        //按键按钮
        playerKey.GetComponent<Button>().onClick.AddListener(TextInputPlayerKey);
        keyboard = Keyboard.current;
        inputAction = inputActions.FindAction(actionName);
    }
    private void TextInputPlayerKey()
    {
        inputAction.Disable();

        // 启动异步重绑定操作
        var rebindOperation = inputAction.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/leftButton") // 可选：排除鼠标左键
            //.WithExpectedControlType("Key")           // 限制按键类型
            //.WithTimeout(5)                               // 5秒后超时
            .OnComplete(operation =>
            {
                Debug.Log("绑定成功: " + operation.selectedControl.name.ToUpper());
                playerKey.transform.GetChild(0).GetComponent<Text>().text =char.ToUpper(operation.selectedControl.name[0]) + operation.selectedControl.name.Substring(1); // ✔ 根据实际绑定控件更新UI（如 "A" 对应键盘按键）
                InputManager.Instance.SaveBinding(inputAction);
                operation.Dispose();
                inputAction.Enable();
            })
            .OnCancel(operation =>
            {
                Debug.Log("绑定取消");
                operation.Dispose();
                inputAction.Enable();
            })
            .Start(); // ✔ 切记调用.Start()启动
    }
}
