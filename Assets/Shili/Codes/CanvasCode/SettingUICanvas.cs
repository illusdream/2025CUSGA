using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUICanvas : MonoBehaviour
{
    public GameObject panel;//��ʾ�����˿�����ֹ��Ҳ�����
    public List<GameObject> textObject;
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
        (new TimerBuilder(3f, 1))
            .SetTimerType(ETimerType.RealTime)
            .SetOnFinish(_ => UnAppear())
            .Register();
    }
    private void UnAppear()
    {
        panel.SetActive(false); 
    }
}
