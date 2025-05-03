using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shili_UIDOTweenAnimationBase : MonoBehaviour
{
    public virtual void OnDisable()
    {
        //Shili_DOTweenManager.Instance.PanelClose(GetComponent<RectTransform>(), transform.parent.GetComponent<CanvasGroup>(),gameObject);
    }
}

public class Shili_UIDOTweenAnimationPanel : Shili_UIDOTweenAnimationBase
{

}
