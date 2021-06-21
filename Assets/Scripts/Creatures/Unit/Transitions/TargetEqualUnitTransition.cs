using UnityEngine;

enum EqualArgument 
{
    Null,
    Something
}
public class TargetEqualUnitTransition : UnitStateTransition
{
    [SerializeField] private EqualArgument _equalArgument;
    public override State GetState()
    {
        Transform unitTarget = unit.Target;
        if (unitTarget == null)
        {
            if (_equalArgument == EqualArgument.Null)
            {
                return TargetState;
            }       
        }
        else
        {
            if (_equalArgument == EqualArgument.Something)
            {
                return TargetState;
            }
        }
       
        return null;
    }
}

