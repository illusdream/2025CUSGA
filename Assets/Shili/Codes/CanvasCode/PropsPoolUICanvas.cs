using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PropsPoolUICanvas : MonoBehaviour
{
    public GameObject prefab;
    public Transform propsGameObject;
    public Transform chioceGameObject;
    /// <summary>
    /// ͨ������Canvas�Ĵ�����ö���ͨ������������������
    /// </summary>
    public void SetOnEnable()
    {
        List<EPropType> allEPropTypes =  PropManager.Instance.GetAllEPropTypes();
        for (int i = 0; i < allEPropTypes.Count - 1; i++)
        {
            GameObject go1 = Instantiate(prefab);
            go1.transform.SetParent(propsGameObject);
            go1.GetComponent<PropChoiceButtonSet>().propPoolGameObject = propsGameObject.gameObject;
            go1.GetComponent<PropChoiceButtonSet>().chiocePropGameObject = chioceGameObject.gameObject;
            if (go1.TryGetComponent<Image>(out var img1) && PropManager.Instance.TryGetPropConfig<BasePropConfig>(allEPropTypes[i+1], out var propConfig1))
            {
                img1.sprite = propConfig1.PropSprite;
                go1.GetComponent<PropChoiceButtonSet>().id = (int)allEPropTypes[i+1];
            }
        }
        GameObject go = Instantiate(prefab);
        go.transform.SetParent(chioceGameObject);
        go.GetComponent<PropChoiceButtonSet>().propPoolGameObject = propsGameObject.gameObject;
        go.GetComponent<PropChoiceButtonSet>().chiocePropGameObject = chioceGameObject.gameObject;
        if (go.TryGetComponent<Image>(out var img) && PropManager.Instance.TryGetPropConfig<BasePropConfig>(allEPropTypes[0], out var propConfig))
        {
            img.sprite = propConfig.PropSprite;
            go.GetComponent<PropChoiceButtonSet>().id =(int) allEPropTypes[0];
        }
    }
    public void SetOnDisable()
    {
        for(int i = 0;i< propsGameObject.childCount; i++)
        {
            Destroy(propsGameObject.GetChild(i).gameObject);
        }
        for (int i = 0; i < chioceGameObject.childCount; i++)
        {
            Destroy(chioceGameObject.GetChild(i).gameObject);
        }
    }

}
