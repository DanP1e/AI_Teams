using UnityEngine;
using System;
public abstract class State : MonoBehaviour
{
    private bool _isInitialized = false;

    public event Action<State> StateChanged;
    public bool IsInitialized { get => _isInitialized; private set => _isInitialized = value; }

    // Вызывается при инициализации и выключении EntityStatesController
    public void EnableState() 
    {
        if (IsInitialized)
            return;
        Init();
        IsInitialized = true;
    }

    // Вызывается при выключении EntityStatesController
    public void DisableState() 
    {
        if (!IsInitialized)
            return;
        Final();
        IsInitialized = false;
    }

    protected void ChangeState(State state)
    {
        StateChanged?.Invoke(state);
    }

    // Нужен для расширения EnableState
    protected virtual void Init() { }

    // Нужен для расширения DisableStatе
    protected virtual void Final() { }

    // Вызывается каждый кадр
    public virtual void OnStateUpdate() { } 
    
    
    
}

