using System;
using System.Collections.Generic;
using ilsFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : ManagerSingleton<CharacterManager>,IManager,IAssemblyForeach
{
    public PlayerController Player1Controller { get;private set; }
    public PlayerController Player2Controller { get;private set; }

    public EntityCollection CharacterCollection { get;private set; }
    
    public bool IsGamePlayState = true;
    
    CharacterConfig _characterConfig;
    public void Init()
    {
        CharacterCollection = EntityManager.Instance.GetEntityCollection(EEntityType.Character);
        _characterConfig = Config.GetConfig<CharacterConfig>();
        
        
        
        InitPlayerAllInputHandler();
        
        
        InitAllPlayers();
    }
    
    public void ForeachCurrentAssembly(Type[] types)
    {
        
    }
    public void Update()
    {
        HandlePlayerMoveInput();
    }

    public void HandlePlayerMoveInput()
    {
        if (InputUtils.GetCurrentInputAction().GamePlay.Player1Move.IsPressed())
        {
            if (TryGetPlayerController(1,out var playerController))
            {
                var commend =new PlayerMoveCommend(playerController,InputUtils.GetCurrentInputAction().GamePlay.Player1Move.ReadValue<Vector2>());
                commend.Execute();
            }
        }
        if (InputUtils.GetCurrentInputAction().GamePlay.Player2Move.IsPressed())
        {
            if (TryGetPlayerController(2,out var playerController))
            {
                var commend =new PlayerMoveCommend(playerController,InputUtils.GetCurrentInputAction().GamePlay.Player2Move.ReadValue<Vector2>());
                commend.Execute();
            }
        }
    }

    public void LateUpdate()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void OnDestroy()
    {
        
    }

    public void OnDrawGizmos()
    {
        
    }

    public void OnDrawGizmosSelected()
    {
       
    }
    
    /// <summary>
    /// 注册所有Player，准备进入游戏状态
    /// </summary>
    public void InitAllPlayers()
    {
        for (int i = 1; i <= 2; i++)
        {
           // var prefab = Asset.Load(_characterConfig.characterPrefab);
            var prefab = _characterConfig.characterPrefabClone;
            var go = GameObject.Instantiate(prefab);
            if (go.TryGetComponent<PlayerController>(out var characterController))
            {
                characterController.Initialize(i);
                switch (i)
                {
                    case 1:
                        Player1Controller = characterController;
                        break;
                    case 2:
                        Player2Controller = characterController;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    
    public bool TryGetPlayerController(int id, out PlayerController controller)
    {

        if (IsGamePlayState)
        {
            switch (id)
            {
                case 1:
                    controller = Player1Controller;
                    return true;
                case 2:
                    controller = Player2Controller;
                    return true;
                default:
                    controller = null;
                    return false;
            }
        }
        controller = null;
        return false;
    }
    
    public List<int> GetAllPlayerID()
    {
        List<int> playerID = new List<int>()
        {
            1,2
        };
        return playerID;
    }

    #region PlayerInputHandler

    private void InitPlayerAllInputHandler()
    {
        InitPlayerBreakTileInputHandler();
        InitPlayerPlaceTileInputHandler();
        InitPlayerUsePropInputHandler();
    }

    private void InitPlayerMoveInputHandler()
    {
       var input =  InputUtils.GetCurrentInputAction();
       input.GamePlay.Player1Move.performed += Player1MoveOnperformed;
       input.GamePlay.Player2Move.performed += Player2MoveOnperformed;
    }
    private void Player1MoveOnperformed(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerMoveCommend(playerController,obj.ReadValue<Vector2>());
            commend.Execute();
        }
    }
    private void Player2MoveOnperformed(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(2,out var playerController))
        {
            var commend =new PlayerMoveCommend(playerController,obj.ReadValue<Vector2>());
            commend.Execute();
        }
    }
    private void InitPlayerBreakTileInputHandler()
    {
        var input =  InputUtils.GetCurrentInputAction();
        input.GamePlay.Player1BreakTile.performed += Player1BreakTileOnperformed;
        input.GamePlay.Player2BreakTile.performed += Player2BreakTileOnperformed;
    }
    private void Player1BreakTileOnperformed(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerBreakTileCommend(playerController);
            commend.Execute();
        }
    }
    private void Player2BreakTileOnperformed(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(2,out var playerController))
        {
            var commend =new PlayerBreakTileCommend(playerController);
            commend.Execute();
        }
    }
    private void InitPlayerPlaceTileInputHandler()
    {
        var input =  InputUtils.GetCurrentInputAction();
        input.GamePlay.Player1PlaceTile.performed += Player1PlaceTileOnperformed;
        input.GamePlay.Player1PlaceTile.performed += Player2PlaceTileOnperformed;
    }

    private void Player1PlaceTileOnperformed(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerPlaceTileCommend(playerController);
            commend.Execute();
        }
    }

    private void Player2PlaceTileOnperformed(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(2,out var playerController))
        {
            var commend =new PlayerPlaceTileCommend(playerController);
            commend.Execute();
        }
    }

    private void InitPlayerUsePropInputHandler()
    {
        var input =  InputUtils.GetCurrentInputAction();
        input.GamePlay.Player1UseProp.performed += Player1UsePropOnperformed;
        input.GamePlay.Player1UseProp.performed += Player1UsePropOnperformed;
    }

    private void Player1UsePropOnperformed(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerUsePropCommend(playerController);
            commend.Execute();
        }
    }

    private void Player2UsePropOnperformed(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(2,out var playerController))
        {
            var commend =new PlayerUsePropCommend(playerController);
            commend.Execute();
        }
    }

    #endregion
}