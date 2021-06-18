using UnityEngine;
using System.Collections.Generic;
using System;
public class StatesController : MonoBehaviour
{
    [SerializeField] private State _currentState;

    private void OnEnable()
    {
        if (_currentState == null)
        {
            Debug.LogError($"{transform.name} -> Field currentState is null! Please add state to the object and put it in currentState.");
            return;
        }
        _currentState.StateChanged += SetState;
        _currentState.EnableState();
    }

    private void OnDisable()
    {
        if (_currentState == null)
        {
            Debug.LogError($"{transform.name} -> Field currentState is null! Please add state to the object and put it in currentState.");
            return;
        }
        _currentState.DisableState();       
    }
    private void Update()
    {
        _currentState.OnStateUpdate();      
    }
    public void SetState(State state) // Устанавливает текущее состояние
    {
        if (state == null) 
        {
            Debug.LogError($"{nameof(state)} argument is null");
            return;
        }
        _currentState.StateChanged -= SetState;
        _currentState?.DisableState();

        _currentState = state;
        _currentState.StateChanged += SetState;
        _currentState.EnableState();      
    }
    public State GetCurrentState() => _currentState;   
    
}

