using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        List<ERandomEventType> allEPropTypes = RandomEventManager.Instance.GetAllRandomEvent();
        for (int i = 0; i < allEPropTypes.Count - 1; i++)
        {
            GameObject go1 = Instantiate(prefab);
            go1.transform.SetParent(propsGameObject);
            go1.GetComponent<RandomEventButtonSet>().propPoolGameObject = propsGameObject.gameObject;
            go1.GetComponent<RandomEventButtonSet>().chiocePropGameObject = chioceGameObject.gameObject;
            if (go1.transform.GetChild(0).TryGetComponent<Text>(out var text1) && RandomEventManager.Instance.TryGetRandomEventConfig(allEPropTypes[i + 1], out var propConfig1))
            {
                text1.text = propConfig1.Name;
                go1.GetComponent<RandomEventButtonSet>().id = i + 1;
            }
        }
        GameObject go = Instantiate(prefab);
        go.transform.SetParent(chioceGameObject);
        go.GetComponent<RandomEventButtonSet>().propPoolGameObject = propsGameObject.gameObject;
        go.GetComponent<RandomEventButtonSet>().chiocePropGameObject = chioceGameObject.gameObject;
        if (go.transform.GetChild(0).TryGetComponent<Text>(out var text) && RandomEventManager.Instance.TryGetRandomEventConfig(allEPropTypes[0], out var propConfig))
        {
            text.text = propConfig.Name;
            go.GetComponent<RandomEventButtonSet>().id = 0;
        }
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
