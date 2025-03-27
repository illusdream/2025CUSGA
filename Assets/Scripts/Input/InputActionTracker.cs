using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionTracker : IDisposable 
{
    public InputAction _trackedAction { get; private set; }

    public float StartRealTime { get; private set; }
    public float StartScaledTime { get; private set; }

    public float EndRealTime { get; private set; }
    public float EndScaledTime { get; private set; }

    public float ContinueRealTime => EndRealTime > StartRealTime ? EndRealTime - StartRealTime :0;
    public float ContinueScaledTime =>EndScaledTime > StartScaledTime ? EndScaledTime - StartScaledTime :0;

    public event Action<InputAction.CallbackContext> started;
    public event Action<InputAction.CallbackContext> performed;
    public event Action<InputAction.CallbackContext> canceled;
    public InputActionTracker(InputAction trackedAction)
    {
        _trackedAction = trackedAction;
        
        _trackedAction.started += TrackedActionOnstarted;
        _trackedAction.performed += TrackedActionOnperformed;
        _trackedAction.canceled += TrackedActionOncanceled;
    }

    private void TrackedActionOnstarted(InputAction.CallbackContext obj)
    {
        StartRealTime = Time.realtimeSinceStartup;
        StartScaledTime = Time.time;
        
        EndRealTime = Time.realtimeSinceStartup;
        EndScaledTime = Time.realtimeSinceStartup;
        
        started?.Invoke(obj);
    }
    private void TrackedActionOnperformed(InputAction.CallbackContext obj)
    {
        EndRealTime = Time.realtimeSinceStartup;
        EndScaledTime = Time.realtimeSinceStartup;
        
        performed?.Invoke(obj);
    }
    private void TrackedActionOncanceled(InputAction.CallbackContext obj)
    {
        EndRealTime = Time.realtimeSinceStartup;
        EndScaledTime = Time.realtimeSinceStartup;
        
        canceled?.Invoke(obj);
    }
    
    public void Update()
    {
        EndRealTime = Time.realtimeSinceStartup;
        EndScaledTime = Time.realtimeSinceStartup;
    }
    
    
    public void Dispose()
    {
        
    }
}

public class InputActionTracker<T> : InputActionTracker where T : struct
{
    public InputActionTracker(InputAction trackedAction) : base(trackedAction)
    {
    }

    public T ActionValue => (_trackedAction?.ReadValue<T>()).GetValueOrDefault();
}