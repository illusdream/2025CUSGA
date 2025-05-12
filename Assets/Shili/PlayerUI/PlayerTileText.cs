using ilsFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ilsFramework.GlobalEventSets;
using static UnityEngine.Rendering.DebugUI;

public class PlayerTileText : MonoBehaviour
{
    [Header("×é¼þ")]
    public PlayerTileHandler tileHandler;
    public PlayerPropContainer playerPropContainer;
    public PlayerHealth playerHealth;
    public PlayerController playerController;
    [Header("UI")]
    public Image image;
    public Text blockText;
    public Text shieldText;
    public Text useTimeText;
    private Color color;
    
    private float timedown=3;
    private void Start()
    {
        if (playerController != null)
        {
            UpColor();
        }
    }

    private void Update()
    {
        blockText.text = playerHealth.healthSources[EHealthSourceType.Life].BaseMaxHealth.ToString();
        if (playerHealth != null)
        {
            shieldText.text = playerHealth.healthSources[EHealthSourceType.Shield].CurrentHealth.ToString("0.0");
        }
        if (tileHandler != null)
        {
            useTimeText.text = tileHandler.PlayerTileCurrentHas.ToString();
        }
        else
        {
            useTimeText.text = 0.ToString();
        }
        timedown -= Time.deltaTime;
        if(timedown < 0)
        {
            image.gameObject.SetActive(false);
        }
        
    }
    private void UpColor()
    {
        if (playerController.PlayerID == 1)
        {
            color = Color.red;
        }
        else if(playerController.PlayerID == 2)
        {
            color = Color.blue;
        }
        image.color = color;
        blockText.color = color;
        shieldText.color = color;
        useTimeText.color = color;
    }
}
