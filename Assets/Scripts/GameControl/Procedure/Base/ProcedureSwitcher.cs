using System;
using System.Collections.Generic;
using ilsFramework;
using Sirenix.OdinInspector;

[Serializable]
public class ProcedureSwitcher
{
        [ShowInInspector]
        protected Dictionary<Type, IProcedureNode> _procedureNodes;
        [ShowInInspector]
        protected Stack<Type> _procedureUsingStack;

        protected IProcedureNode _currentState;

        public ProcedureSwitcher()
        {
                _procedureNodes = new Dictionary<Type, IProcedureNode>();
                _procedureUsingStack = new Stack<Type>();
        }

        public void AddProcedureNode<T>() where T : IProcedureNode
        {
                AddProcedureNode(typeof(T));
        }
        
        private void AddProcedureNode(Type procedureNodeType)
        {
                if (Activator.CreateInstance(procedureNodeType) is IProcedureNode procedureNode && _procedureNodes.TryAdd(procedureNodeType,procedureNode))
                {
                        procedureNode.switcher = this;
                        procedureNode.OnInit();
                }
        }

        public void RemoveProcedureNode<T>() where T : IProcedureNode
        {
                _procedureNodes.Remove(typeof(T));
        }

        public void ChangeProcedureByPopStack()
        {
                while (_procedureUsingStack.TryPeek(out var type))
                {
                        _procedureUsingStack.Pop();
                        ChangeProcedureNode(type);
                        return;
                }
        }
        
        
        public void ChangeProcedureNode<T>() where T : IProcedureNode
        {
                ChangeProcedureNode(typeof(T));
        }

        public void ChangeProcedureNode(Type procedureNodeType)
        {
                if (!_procedureNodes.TryGetValue(procedureNodeType, out IProcedureNode procedureNode))
                {
                        return;
                }
                if (_currentState != null)
                {
                        _currentState.IsExecuting = false;
                        _currentState.OnExit();
                        _procedureUsingStack.Push(_currentState.GetType());
                }
                _currentState = procedureNode;

                _currentState.IsExecuting = true;
                _currentState.OnEnter();

        }

        public void SetCurrentState<T>() where T : IProcedureNode
        {
                if (!_procedureNodes.ContainsKey(typeof(T)))
                {
                        AddProcedureNode<T>();
                }
                _currentState = _procedureNodes[typeof(T)];
        }
        
        public void StartProcedure<T>() where T : IProcedureNode
        {
                if (!_procedureNodes.ContainsKey(typeof(T)))
                {
                        AddProcedureNode<T>();
                }
                _currentState = _procedureNodes[typeof(T)];
                _currentState.OnEnter();
                _currentState.IsExecuting = true;
        }

        public virtual void Update()
        {
                _currentState?.OnUpdate();
        }

        public virtual void FixedUpdate()
        {
                _currentState?.OnFixedUpdate();
        }

        public virtual void LateUpdate()
        {
                _currentState?.OnLateUpdate();
        }

        public void OnDestroy()
        {
                _currentState?.OnExit();

                foreach (var procedureNode in _procedureNodes.Values)
                {
                        procedureNode.OnDestroy();
                }
                _procedureNodes.Clear();
                _procedureUsingStack.Clear();
        }
}