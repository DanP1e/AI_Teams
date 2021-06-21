using UnityEngine;
enum VisibilityChangeEvent
{
    Entered,
    Out
}
public class TargetVisibilityChangedUnitTransition : UnitStateTransition
{
    [SerializeField] private VisibilityChangeEvent _visibilityChangeEvent;

    private bool _isTargetEntered = false;
    private Transform _enteredTarget = null;
    
    public override State GetState()
    {
        if (!_isTargetEntered)
        {
            _enteredTarget = unit.Target;

            if (_enteredTarget != null)
                _isTargetEntered = true;

            if (_visibilityChangeEvent == VisibilityChangeEvent.Entered)
                return TargetState;

            return null;
        }

        if (unit.Target != _enteredTarget)
        {
            return TargetState;
        }

        return null;
    }

   
}

