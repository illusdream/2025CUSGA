using System;
using Cysharp.Threading.Tasks;
using ilsFramework;
using Tiles;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay_InitProcedure : ProcedureNode
{
    public override void OnInit()
    {
        base.OnInit();
    }

    public async override void OnEnter()
    {
        var loadScene = SceneManager.LoadSceneAsync("SampleScene");
        loadScene.allowSceneActivation = false;
        
        var fadeHandler =  UIManager.Instance.GetUIPanel<UI_SystemFadeHandler>();
        fadeHandler.Open();
        fadeHandler.FadeIn(out var duration);
        await UniTask.Delay(TimeSpan.FromSeconds(duration), DelayType.Realtime);
        UIManager.Instance.GetUIPanel<MenuUI>().Close();
        loadScene.allowSceneActivation = true;
        
        
        await loadScene;
        
        
        
        var levelSetting = FindLevelSetting();
        if (!levelSetting)
        {
            //default设置
        }
        
        //先加载一下就好了
        UIManager.Instance.GetUIPanel<InHouseUI>().Open();
        SetTileHealthOverride();
        TileManager.Instance.InitTileHandlers();
        TileManager.Instance.GenerateTiles();

        CharacterManager.Instance.InitAllPlayers(levelSetting.Player1SpawnTransform, levelSetting.Player2SpawnTransform);
        UIManager.Instance.GetUIPanel<UI_SystemFadeHandler>().FadeOut(out var fadeOutDuration);
        
        SetPlayerValues();

        SetRandomSelectConfig();
        StartAllRandomEvent();
        
        await UniTask.Delay(TimeSpan.FromSeconds(fadeOutDuration), DelayType.Realtime);
        
        ChangeState<GamePlay_PlayerObserveProcedure>();
        
        base.OnEnter();
    }

    public  override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnLateUpdate()
    {
        base.OnLateUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public LevelSetting FindLevelSetting()
    {
       return GameObject.Find(LevelSetting.LevelSettingGOName).GetComponent<LevelSetting>();
    }

    private void SetRandomSelectConfig()
    {
        CharacterManager.Instance.SetRandomSelectedPropForPlayer(GameManager.Instance.Player1_RandomSelectedProps, GameManager.Instance.Player2_RandomSelectedProps);
        RandomEventManager.Instance.SetCurrentRandomSelectList(GameManager.Instance.LevelRandomSelectedEvents);
    }

    private void SetPlayerValues()
    {
        CharacterManager.Instance.Player1Controller.SetCurrentHealth(GameManager.Instance.Player1_StartedHealth);
        CharacterManager.Instance.Player1Controller.SetCurrentMaxHealth(GameManager.Instance.Player1_MaxHealth);
        CharacterManager.Instance.Player1Controller.SetEnergyToPropValue(GameManager.Instance.Player1_EnergyCanBeComeProp);
        CharacterManager.Instance.Player1Controller.SetCurrentHasTile(GameManager.Instance.Player1StartHasBlockCount);
        
        CharacterManager.Instance.Player2Controller.SetCurrentHealth(GameManager.Instance.Player2_StartedHealth);
        CharacterManager.Instance.Player2Controller.SetCurrentMaxHealth(GameManager.Instance.Player2_MaxHealth);
        CharacterManager.Instance.Player2Controller.SetEnergyToPropValue(GameManager.Instance.Player2_EnergyCanBeComeProp);
        CharacterManager.Instance.Player2Controller.SetCurrentHasTile(GameManager.Instance.Player2StartHasBlockCount);
    }

    private void SetTileHealthOverride()
    {
        TileManager.Instance.TileHealthOverrideDictionary[typeof(CommonTile)] = GameManager.Instance.CommonTileHealth;
        TileManager.Instance.TileHealthOverrideDictionary[typeof(Tiles.CharactorTile)] = GameManager.Instance.PlayerTileHealth;
    }
    
    private void StartAllRandomEvent()
    {
        TileManager.Instance.CurrentRefreshEmptyInterval = GameManager.Instance.RefreshTileEmptyInterval;
        TileManager.Instance.StartFillRandomRange();
        RandomEventManager.Instance.StartGameCommonRandomEventCycle();
    }
}