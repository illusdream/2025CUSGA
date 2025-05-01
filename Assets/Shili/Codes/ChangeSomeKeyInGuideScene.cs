using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeSomeKeyInGuideScene : MonoBehaviour
{
    public Vector2Int vector21;
    public Vector2Int vector22;
    private MainInputAction inputActions;
    private void Awake()
    {
        inputActions = InputManager.Instance.GetCurrentInputAction();
    }
    private void OnEnable()
    {
        inputActions.GamePlay.Enter.started += OnEnterDown;
        TileManager.Instance.SetTile(false ? typeof(AirTile) : typeof(CommonTile), vector21, EntityID.Empty);
        TileManager.Instance.SetTile(false ? typeof(AirTile) : typeof(CommonTile), vector22, EntityID.Empty);
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        inputActions.GamePlay.Enter.started -= OnEnterDown;
        Time.timeScale = 1f;
    }
    private void OnEnterDown(InputAction.CallbackContext callback)
    {
        gameObject.SetActive(false);
    }
}
