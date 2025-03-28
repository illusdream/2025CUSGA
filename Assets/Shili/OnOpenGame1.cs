using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOpenGame1 : MonoBehaviour
{
    public int playerID;
    private SpriteRenderer spriteRenderer;
    public GameObject canvas;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        playerID = transform.parent.GetComponent<PlayerController>().PlayerID;
        if (playerID == 1)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.blue;
        }
        
    }
    private void OnEnable()
    {
        TimerManager.Instance.RegisterTimer(3f,1,0f, ETimerType.TimeScale,null,null, TimeOver,null);
    }
    private void TimeOver(Timer timer)
    {
        gameObject.SetActive(false);
        canvas.SetActive(true);
    }
}
