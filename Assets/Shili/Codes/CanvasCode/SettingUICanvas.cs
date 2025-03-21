using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUICanvas : MonoBehaviour
{
    public GameObject panel;//提示（大了可以阻止玩家操作）
    private void OnEnable()
    {
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.PromptAppears, OnAppear);
    }
    private void OnDisable()
    {
        GlobalEventCenter.Instance.RemoveListener(GlobalEventSets.PromptAppears, OnAppear);
    }
    private void OnAppear(EventArgs e)
    {
        panel.SetActive(true);
        Invoke("UnAppear",3f);
    }
    private void UnAppear()
    {
        panel.SetActive(false); 
    }
}
