using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI2 : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.GetUIPanel<OnOpenGameUI>().Open();
        UIManager.Instance.GetUIPanel<InHouseUI>().Open();
    }
}
