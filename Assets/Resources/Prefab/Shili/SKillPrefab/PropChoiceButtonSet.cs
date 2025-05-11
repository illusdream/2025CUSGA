using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PropChoiceButtonSet : MonoBehaviour
{
    public int id;//道具标识符
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
