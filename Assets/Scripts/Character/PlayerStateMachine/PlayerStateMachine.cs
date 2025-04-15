using System;
using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine.Video;

[Serializable]
public class PlayerStateMachine
{
        [ShowInInspector]
        private Dictionary<Type, BasePlayerState> states;

        public BasePlayerState currentState => GetCurrentState();

        private BasePlayerState GetCurrentState()
        {
                return states?.GetValueOrDefault(currentStateType,null);
        }
        [ShowInInspector]
        public Type currentStateType { get;private set; }
        
        //事件中心相关

        public PlayerStateMachine()
        {
                states = new Dictionary<Type, BasePlayerState>();
        }
        public virtual void AddState<T>(T state) where T : BasePlayerState
        {
                AddState(typeof(T), state);
        }

        public virtual void AddState(Type type, BasePlayerState state)
        {
                states[type] = state;
                states[type].OnInit();
                states[type].fsm = this;
        }
        
        public virtual void RemoveState<T>() where T : BasePlayerState
        {
                RemoveState(typeof(T));
        }

        public virtual void RemoveState(Type type)
        {
                states.Remove(type);
        }
        
        public  void ChangeState<T>() where T : BasePlayerState
        {
                ChangeState(typeof(T));
        }

        public virtual void ChangeState(Type type)
        {
                currentState?.OnExit();
                SetCurrentState(type);
                currentState.OnEnter();
        }

        public  void SetCurrentState<T>() where T : BasePlayerState
        {
                SetCurrentState(typeof(T));
        }

        public virtual void SetCurrentState(Type type)
        {
                if (states.ContainsKey(type))
                {
                        currentStateType = type;
                }
        }

        public void SetDefaultState<T>() where T : BasePlayerState
        {
                currentStateType = typeof(T);
                currentState.OnEnter();
        }

        public virtual void Update()
        {
                currentState?.OnUpdate();
        }

        public virtual void FixedUpdate()
        {
                currentState?.OnFixedUpdate();
        }

        public virtual void OnDestroy()
        {
                foreach (var state in states.Values)
                {
                        state.OnDestroy();
                }
                states.Clear();
        }
        
}