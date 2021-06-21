using UnityEngine;
public class UnitStateMachine : StateMachine
{
    [SerializeField] protected Unit unit;
    protected override void InitializeState(State state)
    {
        state.TryInitialize(unit);
    }
}

