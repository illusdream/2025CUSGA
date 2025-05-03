using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ilsFrameWork;
using System;
using static TileEvent;
using Tiles;
using ilsFramework;
using static ilsFramework.GlobalEventSets;
using UnityEngine.InputSystem;

public class GuidelinesSceneInit : MonoBehaviour
{
    private MainInputAction inputActions;
    [Header("阶段一")]
    public bool one;
    public ChangeSomeKeyInGuideScene changeSomeKeyInGuideScene;
    public int codes = 2;
    BaseTile tile1;
    BaseTile tile2;
    [Header("阶段三")]
    public Vector2Int vector2Int3;
    [Header("阶段四")]
    public Vector2Int vector2Int4;
    [Header("阶段八")]
    public PlayerController playerController;
    public EPropType ePropType1;
    public EPropType ePropType2;
    private float originalEnergy;
    private void Awake()
    {
        inputActions = InputManager.Instance.GetCurrentInputAction();
        foreach (var action in inputActions)
        {
            inputActions.Disable();
        }
    }
    private void OnEnable()
    {
        TileManager.Instance.AddListener(TileEvent.TileBreakedByPlayer, OnTileDied);
        TileManager.Instance.AddListener(TileEvent.TilePlaced, OnTilePlaced);
        GlobalEventCenter.Instance.AddListener(GlobalEventSets.PlayerSpawn, ChangePlayerEnergy);
    }
    private void OnDisable()
    {
        TileManager.Instance?.RemoveListener(TileEvent.TileBreakedByPlayer, OnTileDied);
        TileManager.Instance?.RemoveListener(TileEvent.TilePlaced, OnTilePlaced);
        GlobalEventCenter.Instance?.RemoveListener(GlobalEventSets.PlayerSpawn, ChangePlayerEnergy);
        foreach (var action in inputActions)
        {
            inputActions.Enable();
        }
    }
    private void OnTileDied(EventArgs e)
    {
        TileBreakedByPlayerEventArgs tileBreakedByPlayerEventArgs = e as TileBreakedByPlayerEventArgs;
        TileManager.Instance.TryGetTile(changeSomeKeyInGuideScene.vector21, out tile1);
        TileManager.Instance.TryGetTile(changeSomeKeyInGuideScene.vector22, out tile2);
        if (tileBreakedByPlayerEventArgs.TilePosition == tile1.Position|| tileBreakedByPlayerEventArgs.TilePosition == tile2.Position)
        {
            codes--;
        }
        if(!one&&codes == 0)
        {
            one = true;
            changeSomeKeyInGuideScene.gameObject.SetActive(true);
        }
        
    }
    private void OnTilePlaced(EventArgs e)
    {
        TilePlacedEventArgs tilePlacedEventArgs = e as TilePlacedEventArgs;
        Debug.Log(tilePlacedEventArgs.TilePosition);
        if (tilePlacedEventArgs.TilePosition == vector2Int3 || tilePlacedEventArgs.TilePosition == vector2Int4)
        {
            changeSomeKeyInGuideScene.gameObject.SetActive(true);
        }
    }
    private void ChangePlayerEnergy(EventArgs e)
    {
        PlayerSpawnEventArgs playerSpawnEventArgs = e as PlayerSpawnEventArgs;
        if(playerSpawnEventArgs.PlayerID == 1)
        {
            playerController = playerSpawnEventArgs.Controller;
            originalEnergy = playerController.EnergyCanBeComeProp;
            playerController.EnergyCanBeComeProp = 99999f;
        }
        
    }
    public void ResomeEnergy()
    {
        playerController.EnergyCanBeComeProp = originalEnergy;
    }
    /// <summary>
    /// 给玩家道具
    /// </summary>
    public void GivePlayerProp()
    {
        playerController.TestProp(ePropType1);
        playerController.TestProp(ePropType2);
    }
}
