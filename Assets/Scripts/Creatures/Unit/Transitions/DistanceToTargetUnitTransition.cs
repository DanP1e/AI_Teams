using UnityEngine;

enum TriggeringEvent
{ 
    InTriggeringZone,
    OutOfTriggeringZone
}

public class DistanceToTargetUnitTransition : UnitStateTransition
{
    [SerializeField] private float _triggeringDistance;
    [SerializeField] private TriggeringEvent _triggeringEvent;

    internal float TriggeringDistance => _triggeringDistance;
    internal TriggeringEvent TriggeringEvent { get => _triggeringEvent; }

    public override State GetState()
    {
        Transform unitTargeet = unit.Target;

        if (unitTargeet == null)
            return null;

        float distanceToTarget = Vector3.Distance(unitTargeet.position, unit.transform.position);

        if (distanceToTarget <= TriggeringDistance && TriggeringEvent == TriggeringEvent.InTriggeringZone)        
            return TargetState;

        if (distanceToTarget > TriggeringDistance && TriggeringEvent == TriggeringEvent.OutOfTriggeringZone)        
            return TargetState;
        
        return null;
    }
}

