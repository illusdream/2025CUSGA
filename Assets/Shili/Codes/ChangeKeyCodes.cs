using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;
using static System.Net.Mime.MediaTypeNames;

public class ChangeKeyCodes : MonoBehaviour
{
    private MainInputAction inputActions;
    private GameObject playerKey;
    private Keyboard keyboard;
    private InputAction inputAction;
    public string actionName;
    public int moveIndex;
    private string keyString;
    private void Awake()
    {

        playerKey = gameObject;
        inputActions = InputManager.Instance.GetCurrentInputAction();
        //按键按钮
        playerKey.GetComponent<Button>().onClick.AddListener(TextInputPlayerKey);
        keyboard = Keyboard.current;
        inputAction = inputActions.FindAction(actionName);
        keyString = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
    }
    private void OnEnable()
    {
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.ResetKey, OnResetKey);
    }
    private void OnDisable()
    {
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.ResetKey, OnResetKey);
    }
    private void OnResetKey(EventArgs e)
    {
        transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = keyString;
    }
    private void TextInputPlayerKey()
    {
        inputAction.Disable();

        // 启动异步重绑定操作
        var rebindOperation = inputAction.PerformInteractiveRebinding();
        rebindOperation.WithTargetBinding(moveIndex);
        rebindOperation.WithControlsExcluding("<mouse>/leftButton"); // 可选：排除鼠标左键
        rebindOperation.WithExpectedControlType("Key");          // 限制按键类型
        rebindOperation.OnComplete(operation =>
        {
            // 检查新选择的控制是否已经被其他动作绑定
            if (IsControlAlreadyBound(char.ToUpper(operation.selectedControl.name[0]) + operation.selectedControl.name.Substring(1)))
            {
                GlobalEventCenter.Instance.BoardCastMessage(GlobalEventSets.PromptAppears, EventArgs.Empty);
                operation.Dispose();
                inputAction.Enable();
                return;
            }
            Debug.Log("绑定成功: " + operation.selectedControl.name.ToUpper());
            string a = char.ToUpper(operation.selectedControl.name[0]) + operation.selectedControl.name.Substring(1);
            playerKey.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = a;
            InputManager.Instance.SaveBinding(inputAction);
            operation.Dispose();
            inputAction.Enable();
        });
        rebindOperation.OnCancel(operation =>
        {
            Debug.Log("绑定取消");
            operation.Dispose();
            inputAction.Enable();
        });
        rebindOperation.Start();
    }

    private bool IsControlAlreadyBound(string selectedControl)
    {
        /*Debug.Log(selectedControl);
        selectedControl = selectedControl.Replace("/","");
        foreach (var action in inputActions)
        {
           // Debug.Log(action.name);
            foreach (var binding in action.bindings)
            {
                string aaaaaaaaaa = binding.path.Replace("/", "");
                aaaaaaaaaa = aaaaaaaaaa.Replace("<", "");
                aaaaaaaaaa = aaaaaaaaaa.Replace(">", "");
                Debug.Log(aaaaaaaaaa);
                if (aaaaaaaaaa == selectedControl)
                {
                    return true;
                }
                
            }
        }
        return false;*/
        List<GameObject> textObjects = transform.parent.parent.parent.parent.parent.GetComponent<SettingUICanvas>().textObject;
        foreach (var i in textObjects)
        {
            if(i.GetComponent<UnityEngine.UI.Text>().text == selectedControl)
            {
                return true;
            }
        }
        return false;
    }

}
