using System;
using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class EntityHandler : MonoBehaviour
{
        [ValueDropdown("GetAllEntityTypes",IsUniqueList = true)]
        [ListDrawerSettings(HideRemoveButton = true,DraggableItems = false)]
        public List<string> EntityTypes = new List<string>();

        private List<string> GetAllEntityTypes()
        {
                return null;
                return Config.GetConfigInEditor<EntityManagerConfig>().EntityTypes.Select((info)=>info.EntityTypeName).ToList();
        }
        
        [ShowInInspector]
        [SerializeField]
        [LabelText("Entity组件")]
        [InfoBox("任何标记为EntityComponent的组件都应该被添加到这个里面")]
        SerializableDictionary<string,Component> components;
        
        
        public bool TryGetComponet<T>(string name, out T component) where T : Component
        {
                if (components.TryGetValue(name, out var _component) && _component is T result)
                {
                        component = result;
                        return true;
                }

                component = null;
                return false;
        }

        
        #region Entity事件中心相关

        //Entity对应的事件中心
        [ShowInInspector]
        Dictionary<(string,EEntityEventScope),HashSet<Action<EventArgs>>> eventHandler;

        public void AddEventListener(string eventType, EEntityEventScope scope, params Action<EventArgs>[] action)
        {
                 
                if (eventHandler.TryGetValue((eventType, scope), out var collection))
                {
                        foreach (var _action in action)
                        {
                                if (!collection.Add(_action))
                                {
                                     $"向事件中心内添加重复的Listener:{_action},GameObject:{gameObject}".ErrorSelf(gameObject);
                                }
                        }
                }
                else
                {
                        var instance = new HashSet<Action<EventArgs>>();
                        eventHandler[(eventType, scope)] = instance;
                        foreach (var _action in action)
                        {
                                if (!instance.Add(_action))
                                {
                                        $"向事件中心内添加重复的Listener:{_action},GameObject:{gameObject}".ErrorSelf(gameObject);
                                }
                        }
                }
        }

        public void RemoveEventListener(string eventType, EEntityEventScope scope, params Action<EventArgs>[] action)
        {
                if (eventHandler.TryGetValue((eventType,scope),out var collection))
                {
                        foreach (var _action in action)
                        {
                                collection.Remove(_action);
                        }
                }
        }

        public void BroadcastEvent(string eventType, EEntityEventScope scope, EventArgs args)
        {
                if (eventHandler.TryGetValue((eventType,scope),out var collection))
                {
                        foreach (var action in collection)
                        {
                                action?.Invoke(args);
                        }
                }
        }

        #endregion

        public void Reset()
        {
                components = new SerializableDictionary<string, Component>();
        }

        public void Start()
        {
                eventHandler = new Dictionary<(string,EEntityEventScope),HashSet<Action<EventArgs>>>();
                InitializedEntityComponents();
                InitEntityToManager();
        }

        private void InitializedEntityComponents()
        {
                foreach (var value in components.Values)
                {
                        if (value is EntityComponent entityComponent)
                        {
                                entityComponent.OnInitialized(this);
                        }
                }
        }

        private void InitEntityToManager()
        {
                
        }

        public void Awake()
        {
                
        }

        public void Update()
        {
                
        }

        public void FixedUpdate()
        {
                
        }

        public void LateUpdate()
        {
                
        }

        public void OnDestroy()
        {
                OnDestroyForEntityComponents();
        }

        private void OnDestroyForEntityComponents()
        {
                foreach (var component in components.Values)
                {
                        if (component is EntityComponent entityComponent)
                        {
                                entityComponent.OnEntityDestroy(this);
                        }
                }
        }

        private void UnregisterEntityFromManager()
        {
                
        }
}