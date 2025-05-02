using System;
using ilsFramework;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class PlayerController : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerController;

        public Transform directionTransform;
        
        public SpriteRenderer spriteRenderer;
        
        public Animator animator;
        public PlayerVisualController visualController;
        public Rigidbody2D rigidbody2D;
        
        public PlayerInputHandler playerInputHandler;
        [ShowInInspector]
        public int PlayerID { get;private set; }

        public float EnergyCanBeComeProp =100;
        
        public bool CanBeControlled { get;private set; }
        [ShowInInspector]
        private PlayerStateMachine stateMachine;
        
        public TimelineAsset DigAsset;

        public TimelineAsset IntoBlackHoleTimelineAsset;
        
        public TimerCollection timerCollection;

        public bool CanSwitchPropUse;

        public bool CanMove;

        public bool IgnoreUsePropInputCache = false;

        public bool CanUpdatePlayerDirection =true;

        public Color PlayerColor;

 
        
        public void Initialize(int playerID)
        {
                timerCollection = new TimerCollection();
                PlayerID = playerID;
                var actions = InputManager.Instance.GetCurrentInputAction().GamePlay;
                CanUpdatePlayerDirection = true;
                switch (playerID)
                {
                        case 1:
                                playerInputHandler = new PlayerInputHandler(actions.Player1Move, actions.Player1UseProp, actions.Player1BreakTile,
                                        actions.Player1PlaceTile,actions.Player1ChangeProp);
                                UpdatePlayerDirection(Vector2.right);
                                break;
                        case 2:
                                playerInputHandler = new PlayerInputHandler(actions.Player2Move, actions.Player2UseProp, actions.Player2BreakTile,
                                        actions.Player2PlaceTile,actions.Player1ChangeProp);
                                UpdatePlayerDirection(Vector2.left);
                                break;
                        default:
                                break;
                }
                
                CanSwitchPropUse = true;
                SetCanMove(true);

                playerInputHandler.SwitchProp.performed+= SwitchPropOnperformed;
                
                stateMachine = new PlayerStateMachine();
                stateMachine.AddState(new PlayerMoveState(handler,this));
                stateMachine.AddState(new PlayerDigState(handler,this));
                stateMachine.AddState(new PlayerPlaceTileState(handler,this));
                stateMachine.AddState(new PlayerUsePropState(handler,this));
                stateMachine.AddState(new PlayerDontControlState(handler,this));
                stateMachine.AddState(new PlayerInBlackHoleState(handler,this));
                stateMachine.SetDefaultState<PlayerMoveState>();
        }




        public void Update()
        {
                if (!CanBeControlled && stateMachine.currentStateType != typeof(PlayerDontControlState))
                {
                        stateMachine.ChangeState<PlayerDontControlState>();
                }
                
                var dir = playerInputHandler.LastActiveMoveDirection;
                var rot = Mathf.Atan2(dir.y, dir.x);
                directionTransform.rotation = quaternion.Euler(0,0,rot);
                stateMachine?.Update();
        }

        public void FixedUpdate()
        {
                stateMachine?.FixedUpdate();
                
                if (handler.TryGetComponet(EntityComponetUsage.EnergyContainer,out PlayerEnergyContainer playerEnergyContainer)
                    && handler.TryGetComponet(EntityComponetUsage.PropContainer,out BasePropContainer playerPropContainer))
                {
                        if (playerEnergyContainer.CurrentEnergy > EnergyCanBeComeProp && !playerPropContainer.IsFullProp())
                        {
                                playerPropContainer.TryInputProp(PropManager.Instance.CreateRandomProp());
                                playerEnergyContainer.CumsumEnergy(EnergyCanBeComeProp);
                        }
                }
        }
        public void UpdatePlayerMoveAnimation(Vector2 playerMoveDirection)
        {
                if (CanMove)
                {
                        var x = math.sign(playerMoveDirection.x);
                        var y = math.sign(playerMoveDirection.y);
                        animator?.SetFloat("XSpeed",Mathf.Abs(x));
                        animator?.SetFloat("YSpeed",y);
                }
        }

        public void UpdatePlayerDirection(Vector2 playerMoveDirection)
        {
                if (!CanUpdatePlayerDirection)
                {
                        return;
                }
                var x = math.sign(playerMoveDirection.x);
                visualController.SetRotation( x==0 ? visualController.Rotation: (x < 0 ? 180 :0));
        }


        public bool IsAlive()
        {
                if (handler.TryGetComponet(EntityComponetUsage.Health,out BaseHealthComponent health))
                {
                     return   health.GetCurrentHealth() > 0;
                }
                return false;
        }

        public void SetCanBeControlled(bool canBeControlled)
        {
                CanBeControlled = canBeControlled;
                if (handler.TryGetComponet(EntityComponetUsage.Moveable,out PlayerMoveComponent playerMoveComponent))
                {
                        playerMoveComponent.CanBeControlled = canBeControlled;
                }
        }

        public void SetCanMove(bool canMove)
        {
                CanMove = canMove;
                if (handler.TryGetComponet(EntityComponetUsage.Moveable,out PlayerMoveComponent playerMoveComponent))
                {
                        playerMoveComponent.CanMove = canMove;
                }
        }

        public void SetPlayerSpriteColor(Color color)
        {
                PlayerColor = color;
                spriteRenderer.color = color;
        }

        public void OnDestroy()
        {
                stateMachine?.OnDestroy();
        }

        private void SwitchPropOnperformed(InputAction.CallbackContext obj)
        {
                if (CanSwitchPropUse && handler.TryGetComponet(EntityComponetUsage.PropContainer,out PlayerPropContainer container))
                {
                        container.MoveCurrentUseProp();
                }
        }
        [Button]
        public void TestProp(EPropType propType)
        {
                if (handler.TryGetComponet(EntityComponetUsage.PropContainer,out BasePropContainer playerPropContainer))
                {
                        playerPropContainer.TryInputProp(PropManager.Instance.CreateTargetProp(propType));
                }

        }

        #region InBlackHole

        public void TryInToBlackHole()
        {
                if (stateMachine.currentStateType != typeof(PlayerInBlackHoleState))
                {
                        stateMachine.ChangeState<PlayerInBlackHoleState>();
                }
        }

        #endregion
}