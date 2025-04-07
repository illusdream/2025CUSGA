using System;
using Cysharp.Threading.Tasks;
using ilsFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;

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
        TileManager.Instance.InitTileHandlers();
        TileManager.Instance.GenerateTiles();
        TileManager.Instance.StartFillRandomRange();
        CharacterManager.Instance.InitAllPlayers(levelSetting.Player1SpawnTransform, levelSetting.Player2SpawnTransform);
 
        
        
        UIManager.Instance.GetUIPanel<UI_SystemFadeHandler>().FadeOut(out var fadeOutDuration);
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

    public async override void OnExit()
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
}