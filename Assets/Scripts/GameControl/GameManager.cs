using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameManager : ManagerSingleton<GameManager>,IManager
{
    [ShowInInspector]
    ProcedureSwitcher procedureSwitcher;
    
    GameControlConfig gameControlConfig;
    
    public bool GameProcedureEnabled { get; private set; }
    
    //一些设置性的东西
    
    public List<EPropType> Player1_RandomSelectedProps { get; private set; }
    
    public List<EPropType> Player2_RandomSelectedProps { get; private set; }
    
    public List<ERandomEventType> LevelRandomSelectedEvents { get; private set; }

    public int Player1_StartedHealth;
    public int Player2_StartedHealth;
    
    public int Player1_MaxHealth;
    public int Player2_MaxHealth;

    public int Player1_EnergyCanBeComeProp;
    public int Player2_EnergyCanBeComeProp;

    public int Player1StartHasBlockCount;
    public int Player2StartHasBlockCount;

    public float RefreshTileEmptyInterval;

    public int PlayerTileHealth;
    
    public int CommonTileHealth;
    public void Init()
    {
        gameControlConfig = Config.GetConfig<GameControlConfig>();
        
        SetDefaultConfigs();

        InitGameProcedureStateMachine();
    }

    public void Update()
    {
        if (GameProcedureEnabled)
        {
            procedureSwitcher.Update();
        }
    }

    public void LateUpdate()
    {
        if (GameProcedureEnabled)
        {
            procedureSwitcher.LateUpdate();
        }
    }

    public void FixedUpdate()
    {
        if (GameProcedureEnabled)
        {
            procedureSwitcher.FixedUpdate();
        }
    }

    public void OnDestroy()
    {
        procedureSwitcher.OnDestroy();
    }

    public void OnDrawGizmos()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
        
    }

    public void InitGameProcedureStateMachine()
    {
        GameProcedureEnabled = gameControlConfig.EnableCommenProcedure;
        
        procedureSwitcher = new ProcedureSwitcher();
        
        procedureSwitcher.AddProcedureNode<StartMenuProcedure>();
        procedureSwitcher.AddProcedureNode<GamePlayProcedure>();
        procedureSwitcher.AddProcedureNode<GamePlay_GuidelinesProcedure>();
        if (GameProcedureEnabled)
        {
            procedureSwitcher.StartProcedure<StartMenuProcedure>();
        }
    }
    [Button]
    public void StartGameProcedureStateMachine()
    {
        if (!GameProcedureEnabled)
        {
            GameProcedureEnabled = true;
            procedureSwitcher.StartProcedure<StartMenuProcedure>();
        }
    }
    [Button]
    public void StopGameProcedureStateMachine()
    {
        if (GameProcedureEnabled)
        {
            GameProcedureEnabled = false;
        }
    }
    [Button]
    public void RestartGame()
    {
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToRestartGamePlay,EventArgs.Empty);
    }
    [Button]
    public void ToMainMenu()
    {
        GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.OrderToSwitchToMainMenu,EventArgs.Empty);
    }


    public void SetDefaultConfigs()
    {
        Player1_RandomSelectedProps = PropManager.Instance.GetDefaultBeSelectRandomPropList();
        Player2_RandomSelectedProps = PropManager.Instance.GetDefaultBeSelectRandomPropList();
        LevelRandomSelectedEvents = RandomEventManager.Instance.GetDefaultRandomSelectList();
        
        Player1_MaxHealth = gameControlConfig.MaxHealth;
        Player2_MaxHealth = gameControlConfig.MaxHealth;
        
        Player1_StartedHealth = gameControlConfig.StartedHealth;
        Player2_StartedHealth = gameControlConfig.StartedHealth;
        
        Player1_EnergyCanBeComeProp = gameControlConfig.EnergyCanBeComeProp;
        Player2_EnergyCanBeComeProp = gameControlConfig.EnergyCanBeComeProp;
        
        Player1StartHasBlockCount = gameControlConfig.StartHasBlockCount;
        Player2StartHasBlockCount = gameControlConfig.StartHasBlockCount;

        RefreshTileEmptyInterval = Config.GetConfig<TileManagerConfig>().RefreshEmptyInterval;
        
        CommonTileHealth = gameControlConfig.CommonTileHealth;
        PlayerTileHealth = gameControlConfig.PlayerTileHealth;
    }
    
    public void SetPlayer1_RandomSelectedProps(List<EPropType> propTypes)
    {
        Player1_RandomSelectedProps = propTypes.ToList();
    }

    public void SetPlayer2_RandomSelectedProps(List<EPropType> propTypes)
    {
        Player2_RandomSelectedProps = propTypes.ToList();
    }

    public void SetLevelRandomSelectedEvents(List<ERandomEventType> eventTypes)
    {
        LevelRandomSelectedEvents = eventTypes.ToList();
    }
}