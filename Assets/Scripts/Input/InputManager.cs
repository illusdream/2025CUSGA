using System.Collections.Generic;
using System.Linq;
using ilsFramework;
using Sirenix.OdinInspector;
using SQLite4Unity3d;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : ManagerSingleton<InputManager>,IManager
{
    public string InputModifierDataBaseName = "InputModifierDB";
    
    private MainInputAction _mainInputAction;
    public void Init()
    {
        _mainInputAction = new MainInputAction();
        

        using (var connection=  DataBase.GetPersistentConnection(InputModifierDataBaseName))
        {
            if (connection.TryGetTable<InputModifierInfo>(out var value))
            {
                LoadAllModifierAction(value.ToList());
            }
        }
        _mainInputAction.GamePlay.Enable();
    }

    public void Update()
    {

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

    private void LoadAllModifierAction(List<InputModifierInfo> modifiers)
    {
        foreach (var info in modifiers)
        {
            var action = _mainInputAction.FindAction(info.GUID);
            action.LoadBindingOverridesFromJson(info.ModifierJson);
        }
    }

    public void SaveBinding(InputAction action)
    {
        if (_mainInputAction.Contains(action))
        {
            using (var connection = GetInputModifierConnection())
            {
                connection.CreateTable<InputModifierInfo>();
                if (connection.TryGetTable<InputModifierInfo>(out _))
                {
                    connection.InsertOrReplace(new InputModifierInfo() { GUID = action.id.ToString(), ModifierJson = action.SaveBindingOverridesAsJson() });
                }
            }

        }
    }

    public MainInputAction GetCurrentInputAction()
    {
        return _mainInputAction;
    }

    private SQLiteConnection GetInputModifierConnection()
    {
        return DataBase.GetPersistentConnection(InputModifierDataBaseName);
    }
}