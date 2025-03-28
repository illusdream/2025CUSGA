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

        // 保存原始绑定状态
        string originalBindings = inputAction.SaveBindingOverridesAsJson();

        var rebindOperation = inputAction.PerformInteractiveRebinding();
        rebindOperation.WithTargetBinding(moveIndex);
        rebindOperation.WithControlsExcluding("<mouse>/leftButton");
        rebindOperation.WithExpectedControlType("Key");

        rebindOperation.OnComplete(operation =>
        {
            // 先不应用新绑定，获取候选按键
            InputControl newControl = operation.selectedControl;
            bool conflict = IsControlAlreadyBound(newControl);

            // 始终先回滚到原始状态
            inputAction.LoadBindingOverridesFromJson(originalBindings);

            if (conflict)
            {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
                GlobalEventCenter.Instance.BoardCastMessage(GlobalEventSets.PromptAppears, EventArgs.Empty);
                operation.Dispose();
                inputAction.Enable();
                return;
=======
                Debug.Log("冲突");
                GlobalEventCenter.Instance.BoradCastMessage(GlobalEventSets.PromptAppears, EventArgs.Empty);
>>>>>>> Stashed changes
=======
                Debug.Log("冲突");
                GlobalEventCenter.Instance.BoradCastMessage(GlobalEventSets.PromptAppears, EventArgs.Empty);
>>>>>>> Stashed changes
            }
            else
            {
                // 安全应用新绑定
                inputAction.ApplyBindingOverride(moveIndex, newControl.path);
                Debug.Log("绑定成功: " + newControl.name.ToUpper());
                string a = FormatKeyName(newControl.name);
                playerKey.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = a;
                InputManager.Instance.SaveBinding(inputAction);
            }

            operation.Dispose();
            inputAction.Enable();
        });

        rebindOperation.OnCancel(operation =>
        {
            inputAction.LoadBindingOverridesFromJson(originalBindings); // 注意这里也要回滚
            Debug.Log("绑定取消");
            operation.Dispose();
            inputAction.Enable();
        });

        rebindOperation.Start();

        // 新增格式化方法
        string FormatKeyName(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

    }
    // 新增路径标准化方法
    static string NormalizeControlPath(string path)
    {
        // 统一格式：移除尖括号和开头斜杠（处理类似 <Keyboard>/w 和 /Keyboard/w 的情况）
        return path.Replace("<", "").Replace(">", "").TrimStart('/').ToLower();
    }

    // 改进的冲突检测逻辑
    bool IsControlAlreadyBound(InputControl targetControl)
    {
        string targetPath = NormalizeControlPath(targetControl.path);
        foreach (var binding in inputAction.bindings)
        {
            string bindingPath = NormalizeControlPath(binding.effectivePath);
            if (bindingPath == targetPath)
                return true;
        }
        return false;
    }

    /*private bool IsControlAlreadyBound(string selectedControl)
    {
        *//*Debug.Log(selectedControl);
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
        return false;*//*
        List<GameObject> textObjects = transform.parent.parent.parent.parent.parent.GetComponent<SettingUICanvas>().textObject;
        foreach (var i in textObjects)
        {
            if(i.GetComponent<UnityEngine.UI.Text>().text == selectedControl)
            {
                return true;
            }
        }
        return false;
    }*/

}
