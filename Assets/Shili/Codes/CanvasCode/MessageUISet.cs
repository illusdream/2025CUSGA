using ilsFramework;
using UnityEngine;

public class MessageUISet : MonoBehaviour
{
    public float waitTime=3f;
    private float currentWaitTime;
    private bool isWait;
    private void Awake()
    {
        currentWaitTime = waitTime;
    }
    private void Update()
    {
        if (isWait)
        {
            currentWaitTime -= Time.deltaTime;
            if(currentWaitTime <= 0)
            {
                isWait = false;
                UIManager.Instance.GetUIPanel<MessageUI>().Close();
                currentWaitTime = waitTime;
            }
        }
    }
    public void SetOnEnable()
    {
        isWait = true;
    }
}