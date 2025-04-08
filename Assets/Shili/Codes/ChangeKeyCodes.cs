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
    private GameObject playerKey;
    public string actionName;
    public int moveIndex;
    private string keyString;
    private void Awake()
    {
        playerKey = gameObject;
        //按键按钮
        playerKey.GetComponent<Button>().onClick.AddListener(SetAndUse);
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
   private void SetAndUse()
    {
        shili_InputManager.Instance.SetAndUse(playerKey, actionName, moveIndex);
    }
}
