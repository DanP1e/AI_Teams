using UnityEngine;
public abstract class StateTransiton : MonoBehaviour
{
    [SerializeField] private State _targetState;
    public State TargetState => _targetState;
    public abstract State GetState();
    public abstract void Init(params object[] initializationParams);
}

