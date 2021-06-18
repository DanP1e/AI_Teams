using UnityEngine;

public class IdleUnitState : UnitState
{
    public UnitState WhenTargetDetectedState;
  
    protected override void OnTargetDetected(Transform target)
    {
        ChangeState(WhenTargetDetectedState);
    }
}

