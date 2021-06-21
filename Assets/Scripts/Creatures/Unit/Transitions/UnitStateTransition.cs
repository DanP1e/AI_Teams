using UnityEngine;
public abstract class UnitStateTransition : StateTransiton
{
    protected Unit unit = null;
    public override void Init(params object[] initializationParams)
    {
        unit = new ParamsToTypeConverter<Unit>(initializationParams).Get();
    }
}

