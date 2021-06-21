using UnityEngine;
using System;
public abstract class UnitState : State
{
    protected Unit unit = null;

    protected override void OnEntered()
    {
        base.OnEntered();
        if (!IsInitialized)
        {
            Debug.LogError($"\"{this.GetType().Name}\" state not initialized ");
            enabled = false;
        }
    }

    protected override void Initialize(params object[] initializationParams)
    {      
        unit = new ParamsToTypeConverter<Unit>(initializationParams).Get();

        foreach (StateTransiton transiton in stateTransitons)
        {
            transiton.Init(unit);
        }
    }

}

