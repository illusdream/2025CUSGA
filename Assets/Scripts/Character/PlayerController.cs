using System;
using ilsFramework;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : EntityComponent
{
        public override string TargetUsage => EntityComponetUsage.playerController;

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
        
        public TimerCollection timerCollection;

        public void Initialize(int playerID)
        {
                timerCollection = new TimerCollection();
                PlayerID = playerID;
                var actions = InputManager.Instance.GetCurrentInputAction().GamePlay;
                switch (playerID)
                {
                        case 1:
                                playerInputHandler = new PlayerInputHandler(actions.Player1Move, actions.Player1UseProp, actions.Player1BreakTile,
                                        actions.Player1PlaceTile);
                                break;
                        case 2:
                                playerInputHandler = new PlayerInputHandler(actions.Player2Move, actions.Player2UseProp, actions.Player2BreakTile,
                                        actions.Player2PlaceTile);
                                break;
                        default:
                                break;
                }
                
                stateMachine = new PlayerStateMachine();
                stateMachine.AddState(new PlayerMoveState(handler,this));
                stateMachine.AddState(new PlayerDigState(handler,this));
                stateMachine.AddState(new PlayerPlaceTileState(handler,this));
                stateMachine.AddState(new PlayerUsePropState(handler,this));
                stateMachine.SetDefaultState<PlayerMoveState>();
        }
        
        

        public void Update()
        {
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
                if (CanBeControlled)
                {
                        var x = math.sign(playerMoveDirection.x);
                        var y = math.sign(playerMoveDirection.y);
                        animator?.SetFloat("XSpeed",Mathf.Abs(x));
                        animator?.SetFloat("YSpeed",y);
                }
        }

        public void UpdatePlayerDirection(Vector2 playerMoveDirection)
        {
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

        public void SetPlayerSpriteColor(Color color)
        {
                spriteRenderer.color = color;
        }

        public void OnDestroy()
        {
                stateMachine?.OnDestroy();
        }
}