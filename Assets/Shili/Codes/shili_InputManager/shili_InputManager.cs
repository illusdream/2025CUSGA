using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class shili_InputManager : MonoBehaviour
{
    private static shili_InputManager instance;
    private MainInputAction inputActions;
    private InputAction inputAction;
    private int moveIndex;
    private GameObject playerKey;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    public bool isGuide;
    public static shili_InputManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("shili_InputManager").AddComponent< shili_InputManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    public shili_InputManager()
    {
        inputActions = InputManager.Instance.GetCurrentInputAction();
    }
    public void SetAndUse(GameObject go,string s,int index=0)
    {
        playerKey = go;
        moveIndex = index;
        inputAction = inputActions.FindAction(s);
        TextInputPlayerKey();
    }
    private void TextInputPlayerKey()
    {
        if (rebindOperation != null && rebindOperation.started)
        {
            Debug.Log("点击了其他地方");
            rebindOperation.Cancel();
            rebindOperation = null;
        }
        inputActions.Disable();
        // 保存原始绑定状态
        string originalBinding = inputAction.SaveBindingOverridesAsJson();
        rebindOperation = inputAction.PerformInteractiveRebinding();
        rebindOperation.WithTargetBinding(moveIndex)
        //不希望接收鼠标的输入
        .WithControlsExcluding("Mouse")
        //监听其它输入的间隔
        .OnMatchWaitForAnother(0.1f);
        rebindOperation.WithExpectedControlType("Key");
        rebindOperation.OnComplete(operation =>
        {
            InputControl newControl = operation.selectedControl;
            // 回滚临时绑定
            inputAction.LoadBindingOverridesFromJson(originalBinding);
            bool conflict = IsControlAlreadyBound(newControl);
            if (conflict)
            {
                GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.PromptAppears, EventArgs.Empty);
            }
            else
            {
                // 安全应用新绑定（仅在无冲突时）
                inputAction.ApplyBindingOverride(moveIndex, newControl.path);
                string a = FormatKeyName(newControl.name);
                playerKey.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = a;
                InputManager.Instance.SaveBinding(inputAction);
                rebindOperation = null;
            }

            operation.Dispose();
            inputActions.Enable();
        });

        rebindOperation.OnCancel(operation =>
        {
            inputAction.LoadBindingOverridesFromJson(originalBinding); // 注意这里也要回滚
            Debug.Log("绑定取消");
            operation.Dispose();
            inputActions.Enable();
            rebindOperation = null;
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
    private bool IsControlAlreadyBound(InputControl targetControl)
    {
        string targetPath = NormalizeControlPath(targetControl.path);
        InputActionMap currentActionMap = inputAction.actionMap;

        if (currentActionMap == null)
            return false;

        foreach (InputAction action in currentActionMap.actions)
        {
            foreach (InputBinding binding in action.bindings)
            {
                // 跳过当前正在修改的绑定
                if (action == inputAction && binding.id == inputAction.bindings[moveIndex].id)
                    continue;

                string bindingPath = NormalizeControlPath(binding.effectivePath);
                if (bindingPath == targetPath)
                    return true;
            }
        }

        return false;
    }
}
