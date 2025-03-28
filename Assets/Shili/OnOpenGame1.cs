using ilsFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOpenGame1 : MonoBehaviour
{
    public int playerID;
    private SpriteRenderer spriteRenderer;
    public GameObject canvas;
    
    public PlayerController player;
    public float FadeTime =3;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        playerID = player.PlayerID;
        CharacterConfig config = Config.GetConfig<CharacterConfig>();
        if (playerID == 1)
        {
            spriteRenderer.color = config.Player1Color;
        }
        else
        {
            spriteRenderer.color = config.Player2Color;
        }
        
    }
    private void OnEnable()
    {
        TimerManager.Instance.RegisterTimer(FadeTime,1,0f, ETimerType.TimeScale,null,null, TimeOver,null);
    }
    private void TimeOver(Timer timer)
    {
        gameObject.SetActive(false);
        canvas.SetActive(true);
    }
}
