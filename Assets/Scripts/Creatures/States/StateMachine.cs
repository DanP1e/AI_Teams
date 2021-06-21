using UnityEngine;
using System.Collections.Generic;
using System;
public abstract class StateMachine : MonoBehaviour
{
    [SerializeField] private State _currentState;
    public State CurrentState => _currentState;

    private void OnEnable()
    {
        if (_currentState == null)
        {
            Debug.LogError($"{transform.name} -> Field currentState is null! Please add state to the object and put it in currentState.");
            return;
        }
        InitializeState(_currentState);
        _currentState.Enter();       
        OnEnabled();
    }
    private void OnDisable()
    {
        if (_currentState == null)
        {
            Debug.LogError($"{transform.name} -> Field currentState is null! Please add state to the object and put it in currentState.");
            return;
        }
        _currentState.Exit();
        OnDisabled();
    }
    private void Update()
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();
        if (nextState == null)
            return;

        _currentState.Exit();
        _currentState = nextState;
        InitializeState(_currentState);
        _currentState.Enter();       
    }
    protected abstract void InitializeState(State state);
    protected virtual void OnEnabled() { }

    protected virtual void OnDisabled() { }
    
    
   

    
    
}

