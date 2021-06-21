using UnityEngine;
public class TargetChangedUnitTransition : UnitStateTransition
{
    Transform oldTraget = null;
    private void OnEnable()
    {
        oldTraget = unit.Target;
    }
    public override State GetState()
    {
        Transform newTarget = unit.Target;
        if (oldTraget == newTarget)
            return null;

        return TargetState;
    }
}
