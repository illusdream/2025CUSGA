using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventlUICanvas : MonoBehaviour
{
    public GameObject prefab;
    public Transform propsGameObject;
    public Transform chioceGameObject;
    /// <summary>
    /// 通过控制Canvas的代码调用而非通过生命周期主动调用
    /// </summary>
    public void SetOnEnable()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject go1 = Instantiate(prefab);
            go1.transform.SetParent(propsGameObject);
            go1.GetComponent<RandomEventButtonSet>().propPoolGameObject = propsGameObject.gameObject;
            go1.GetComponent<RandomEventButtonSet>().chiocePropGameObject = chioceGameObject.gameObject;
        }
        GameObject go = Instantiate(prefab);
        go.transform.SetParent(chioceGameObject);
        go.GetComponent<RandomEventButtonSet>().propPoolGameObject = propsGameObject.gameObject;
        go.GetComponent<RandomEventButtonSet>().chiocePropGameObject = chioceGameObject.gameObject;
    }
    public void SetOnDisable()
    {
        for (int i = 0; i < propsGameObject.childCount; i++)
        {
            Destroy(propsGameObject.GetChild(i).gameObject);
        }
        for (int i = 0; i < chioceGameObject.childCount; i++)
        {
            Destroy(chioceGameObject.GetChild(i).gameObject);
        }
    }
}
