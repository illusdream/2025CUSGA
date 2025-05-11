using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RandomEventButtonSet : MonoBehaviour
{
    public int id;//随机事件标识符，还不清楚具体填什么才可以让列表识别
    public GameObject propPoolGameObject;
    public GameObject chiocePropGameObject;
    public void OnClick()
    {
        if (transform.parent == propPoolGameObject.transform)
        {
            transform.SetParent(chiocePropGameObject.transform);
        }
        else
        {
            transform.SetParent(propPoolGameObject.transform);
        }
    }
}
