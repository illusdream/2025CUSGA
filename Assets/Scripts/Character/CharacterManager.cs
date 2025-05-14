using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using ilsFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : ManagerSingleton<CharacterManager>,IManager,IAssemblyForeach
{
    public PlayerController Player1Controller { get;private set; }
    public PlayerController Player2Controller { get;private set; }

    public EntityCollection CharacterCollection { get;private set; }
    public EntityCollection Flyable { get;private set; }
    
    public bool IsGamePlayState = true;
    
    CharacterConfig _characterConfig;

    private EdgeCollider2D playRangeInnerEdgeLeft;
    private EdgeCollider2D playRangeInnerEdgeDown;
    private EdgeCollider2D playRangeInnerEdgeRight;
    private EdgeCollider2D playRangeInnerEdgeUp;

    private PolygonCollider2D playRangeBigSizeCheck;
    
    private const string playRangeGOName = "PlayRange";
    
    private GameObject playRangeGO;

    private List<EntityHandler> PlayerInEdgeOfPlayRangeResult;
    private List<EntityHandler> PlayerInBoundOfPlayRangeResult;
    
    private bool playRangeLimitEnable = false;
    public bool PlayRangeLimitEnable => playRangeLimitEnable;

    private InputActionTracker player1BreakActionTracker;
    private InputActionTracker player2BreakActionTracker;
    
    public void Init()
    {
        CharacterCollection = EntityManager.Instance.GetEntityCollection(EEntityType.Character);
        Flyable = EntityManager.Instance.GetEntityCollection(EEntityType.Flyable);
        _characterConfig = Config.GetConfig<CharacterConfig>();
        
        PlayerInBoundOfPlayRangeResult = new List<EntityHandler>();
        PlayerInEdgeOfPlayRangeResult = new List<EntityHandler>();
        InitializePlayerPlayRange();
        
        InitPlayerAllInputHandler();

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
        CheckPlayerIsOutOfPlayRange();
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
    public void InitAllPlayers(Transform player1SpawnPoint, Transform player2SpawnPoint)
    {
        for (int i = 1; i <= 2; i++)
        {
            var prefab = _characterConfig.characterPrefabClone;
            var SS = SpawnSource.SpawnBySystem(player1SpawnPoint.position);
            var go = Entity.Instantiate(prefab,SS,new Vector3(0, 0, 0),Quaternion.identity);
            if (go.TryGetComponent<PlayerController>(out var characterController))
            {
                characterController.Initialize(i);
                characterController.SetCanBeControlled(false);
                switch (i)
                {
                    case 1:
                        Player1Controller = characterController;
                        characterController.SetPlayerSpriteColor(_characterConfig.Player1Color);
                        go.transform.position = player1SpawnPoint.position;
                        go.transform.rotation = player1SpawnPoint.rotation;
                        go.transform.localScale = player1SpawnPoint.localScale;
                        break;
                    case 2:
                        Player2Controller = characterController;
                        characterController.SetPlayerSpriteColor(_characterConfig.Player2Color);
                        go.transform.position = player2SpawnPoint.position;
                        go.transform.rotation = player2SpawnPoint.rotation;
                        go.transform.localScale = player2SpawnPoint.localScale;
                        break;
                    default:
                        break;
                }
                var psea = new GlobalEventSets.PlayerSpawnEventArgs(characterController, i, SS);
                GlobalEventCenter.Instance.BroadcastMessage(GlobalEventSets.PlayerSpawn, psea);
            }
        }
    }

    public void SetRandomSelectedPropForPlayer(List<EPropType> player1Props, List<EPropType> player2Props)
    {
        Player1Controller?.SetBeSelectedRandomProps(player1Props);
        Player2Controller?.SetBeSelectedRandomProps(player2Props);
    }
    
    public void SetAllPlayerCanBeControlled(bool canBeControlled)
    {
        foreach (var playerController in GetAllPlayers())
        {
            playerController.SetCanBeControlled(canBeControlled);
        }
    }

    public bool IsPlayer1(EntityID id)
    {
        return id == Player1Controller.ID;
    }

    public bool IsPlayer2(EntityID id)
    {
        return id == Player2Controller.ID;
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
    
    public bool TryGetPlayerController(EntityID id, out PlayerController controller)
    {

        if (IsGamePlayState)
        {
            if (IsPlayer1(id))
            {
                controller = Player1Controller;
                return true;
            }

            if (IsPlayer2(id))
            {
                controller = Player2Controller;
                return true;
            }
        }
        controller = null;
        return false;
    }
    
    public List<EntityID> GetAllPlayerID()
    {
        return CharacterCollection.Select(player=>player.ID).ToList();
    }

    public IEnumerable<PlayerController> GetAllPlayers()
    {
        return new []{Player1Controller, Player2Controller};
    }

    #region PlayerInputHandler

    
    private void InitPlayerAllInputHandler()
    {
        InitPlayerStopMoveInputHandler();
        InitPlayerBreakTileInputHandler();
        InitPlayerPlaceTileInputHandler();
        InitPlayerUsePropInputHandler();
    }

    private void InitPlayerStopMoveInputHandler()
    {
        var input =  InputUtils.GetCurrentInputAction();
        
        input.GamePlay.Player1Move.canceled += Player1MoveOncanceled;
        input.GamePlay.Player2Move.canceled+= Player2MoveOncanceled;


    }

    private void Player1MoveOncanceled(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerMoveCommend(playerController,InputUtils.GetCurrentInputAction().GamePlay.Player1Move.ReadValue<Vector2>());
            commend.Execute();
        }
    }
    private void Player2MoveOncanceled(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerMoveCommend(playerController,InputUtils.GetCurrentInputAction().GamePlay.Player2Move.ReadValue<Vector2>());
            commend.Execute();
        }
    }

    private void InitPlayerBreakTileInputHandler()
    {
        var input =  InputUtils.GetCurrentInputAction();

        player1BreakActionTracker = new InputActionTracker(input.GamePlay.Player1BreakTile);
        player1BreakActionTracker.started += Player1BreakActionTrackerOnstarted;
        player1BreakActionTracker.canceled += Player1BreakActionTrackerOncanceled;
        player2BreakActionTracker = new InputActionTracker(input.GamePlay.Player2BreakTile);
        player2BreakActionTracker.started += Player2BreakActionTrackerOnstarted;
        player2BreakActionTracker.canceled += Player2BreakActionTrackerOncanceled;
    }
    private void Player1BreakActionTrackerOnstarted(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerBreakStartCommend(playerController);
            commend.Execute();
        }
    }
    private void Player1BreakActionTrackerOncanceled(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerBreakEndCommend(playerController,player1BreakActionTracker);
            commend.Execute();
        }
    }
    private void Player2BreakActionTrackerOnstarted(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(2,out var playerController))
        {
            var commend =new PlayerBreakStartCommend(playerController);
            commend.Execute();
        }
    }
    private void Player2BreakActionTrackerOncanceled(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(2,out var playerController))
        {
            var commend =new PlayerBreakEndCommend(playerController,player2BreakActionTracker);
            commend.Execute();
        }
    }
    
    
    
    private void InitPlayerPlaceTileInputHandler()
    {
        var input =  InputUtils.GetCurrentInputAction();
        input.GamePlay.Player1PlaceTile.started += Player1PlaceActionTrackerOnstarted;
        input.GamePlay.Player1PlaceTile.canceled += Player1PlaceActionTrackerOncanceled;
        input.GamePlay.Player2PlaceTile.started += Player2PlaceActionTrackerOnstarted;
        input.GamePlay.Player2PlaceTile.canceled += Player2PlaceActionTrackerOncanceled;
    }

    private void Player1PlaceActionTrackerOnstarted(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerPlaceStartCommend(playerController);
            commend.Execute();
        }
    }
    private void Player1PlaceActionTrackerOncanceled(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(1,out var playerController))
        {
            var commend =new PlayerPlaceEndCommend(playerController);
            commend.Execute();
        }
    }
    private void Player2PlaceActionTrackerOnstarted(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(2,out var playerController))
        {
            var commend =new PlayerPlaceStartCommend(playerController);
            commend.Execute();
        }
    }
    private void Player2PlaceActionTrackerOncanceled(InputAction.CallbackContext obj)
    {
        if (TryGetPlayerController(2,out var playerController))
        {
            var commend =new PlayerPlaceEndCommend(playerController);
            commend.Execute();
        }
    }

    private void InitPlayerUsePropInputHandler()
    {
        var input =  InputUtils.GetCurrentInputAction();
        input.GamePlay.Player1UseProp.performed += Player1UsePropOnperformed;
        input.GamePlay.Player2UseProp.performed += Player2UsePropOnperformed;
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


    #region Player游玩范围

    private void InitializePlayerPlayRange()
    {
        playRangeGO = new GameObject(playRangeGOName);
        playRangeGO.transform.SetParent(ContainerObject.transform);

        playRangeGO.layer = LayerMask.NameToLayer("AOERange");
        
        
        var rangeRect = _characterConfig.PlayerCanPlayRange;
        var leftdown = new Vector2(rangeRect.xMin, rangeRect.yMin);
        var rightdown = new Vector2(rangeRect.xMax, rangeRect.yMin);
        var leftup = new Vector2(rangeRect.xMin, rangeRect.yMax);
        var rightup = new Vector2(rangeRect.xMax, rangeRect.yMax);
        var innerEdgePoints = new List<Vector2>() {leftdown, rightdown, rightup, leftup ,leftdown};

        #region LeftEdge

        GameObject LeftEdgeGO = new GameObject("LeftEdgeGO");
        LeftEdgeGO.transform.SetParent(ContainerObject.transform);
        LeftEdgeGO.layer = LayerMask.NameToLayer("AOERange");
        playRangeInnerEdgeLeft = LeftEdgeGO.AddComponent<EdgeCollider2D>();
        playRangeInnerEdgeLeft.points = new []{leftdown,leftup};

        #endregion 

        #region DownEdge

        GameObject DownEdgeGO = new GameObject("DownEdgeGO");
        DownEdgeGO.transform.SetParent(ContainerObject.transform);
        DownEdgeGO.layer = LayerMask.NameToLayer("AOERange");
        playRangeInnerEdgeDown = DownEdgeGO.AddComponent<EdgeCollider2D>();
        playRangeInnerEdgeDown.points = new []{leftdown,rightdown};

        #endregion 
        
        #region RightEdge

        GameObject RightEdgeGO = new GameObject("RightEdge");
        RightEdgeGO.transform.SetParent(ContainerObject.transform);
        RightEdgeGO.layer = LayerMask.NameToLayer("AOERange");
        playRangeInnerEdgeRight = RightEdgeGO.AddComponent<EdgeCollider2D>();
        playRangeInnerEdgeRight.points = new []{rightup,rightdown};

        #endregion 
        
        #region RightEdge

        GameObject UpEdgeGO = new GameObject("UpEdge");
        UpEdgeGO.transform.SetParent(ContainerObject.transform);
        UpEdgeGO.layer = LayerMask.NameToLayer("AOERange");
        playRangeInnerEdgeUp = UpEdgeGO.AddComponent<EdgeCollider2D>();
        playRangeInnerEdgeUp.points = new []{rightup,leftup};

        #endregion 
        
        
        var boundsPoints = innerEdgePoints.ToList();
        boundsPoints.AddRange(innerEdgePoints.Select(p =>
        {
            var dif = p - rangeRect.center;
            return rangeRect.center + dif * 2;
        }));

        playRangeBigSizeCheck = playRangeGO.AddComponent<PolygonCollider2D>();
        playRangeBigSizeCheck.points = boundsPoints.ToArray();
        
    }

    private void CheckPlayerIsOutOfPlayRange()
    {
        if (!playRangeLimitEnable)
        {
            return;
        }
        EdgeReflect(playRangeInnerEdgeLeft,Vector2.right);
        EdgeReflect(playRangeInnerEdgeDown,Vector2.up);
        EdgeReflect(playRangeInnerEdgeRight,Vector2.left);
        EdgeReflect(playRangeInnerEdgeUp,Vector2.down);
        
        PlayerInBoundOfPlayRangeResult.Clear();
        CharacterCollection.GetEntityInArea(playRangeBigSizeCheck,PlayerInBoundOfPlayRangeResult);
        foreach (var handler in PlayerInBoundOfPlayRangeResult)
        {
            if (handler.TryGetComponet(EntityComponetUsage.Moveable,out PlayerMoveComponent playerMoveComponent)
                &&handler.TryGetComponet(EntityComponetUsage.EntityBaseCollider,out Collider2D playerCollider))
            {
                //找到最小包围盒
                var mul = 1.03f;
                var playerBound = playerCollider.bounds;
                var playRangeSize = _characterConfig.PlayerCanPlayRange.size;
                var boundsize = new Vector3(playRangeSize.x,playRangeSize.y, 0) - new Vector3(playerBound.size.x *mul,playerBound.size.y *mul,0);
                var cBound = new Bounds(_characterConfig.PlayerCanPlayRange.center, boundsize);

               // playerMoveComponent.Rigidbody2D.position =cBound.ClosestPoint(playerMoveComponent.GetEntityPosition());

            }
        }
    }

    private void EdgeReflect(EdgeCollider2D edge, Vector2 reflectNormal)
    {
        int count = 0;
        PlayerInEdgeOfPlayRangeResult.Clear();
        CharacterCollection.GetEntityInArea(edge,PlayerInEdgeOfPlayRangeResult);
        Flyable.GetEntityInArea(edge,PlayerInEdgeOfPlayRangeResult);
        foreach (var handler in PlayerInEdgeOfPlayRangeResult)
        {
            if (handler.TryGetComponet(EntityComponetUsage.Moveable,out BaseEntityMove playerMoveComponent))
            {
                if ((playerMoveComponent.GetEntityVelocity() * reflectNormal).magnitude < _characterConfig.MinCanBounceSpeed)
                {
                    playerMoveComponent.rigidbody2D.velocity *= (Vector2.one - new Vector2(Mathf.Abs(reflectNormal.x), Mathf.Abs(reflectNormal.y)));
                    continue;
                }
                Vector2 reflectedVelocity = Vector2.Reflect(playerMoveComponent.GetEntityVelocity(), reflectNormal);
                playerMoveComponent.SetTargetVelocity(reflectedVelocity * _characterConfig.PlayerRangeEdgeBounciness);
            }
        }

        while (PlayerInEdgeOfPlayRangeResult.Any() && count <20)
        {
            count++;
           foreach (var handler in PlayerInEdgeOfPlayRangeResult)
           {
               handler.transform.position += reflectNormal.Vec3_xy() * 0.001f;
           }
           PlayerInEdgeOfPlayRangeResult.Clear();
           CharacterCollection.GetEntityInArea(edge,PlayerInEdgeOfPlayRangeResult);
        }
    }

    public void EnablePlayRangeLimit()
    {
        playRangeLimitEnable = true;
    }

    public void DisablePlayRangeLimit()
    {
        playRangeLimitEnable = false;
    }
    
    
    #endregion
}