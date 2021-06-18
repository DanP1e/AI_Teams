using UnityEngine;
public class PursuitUnitState : UnitState
{   
    [Header("States")]
    public UnitState WhenTargetLostState;
    public UnitState WhenGoalAchievedState;
    public UnitState WhenTargetAbsentState;

    [Header("Pursuit preferences")]
    public float StopDistance = 1.5f;

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
        if (unit.Target == null) { ChangeState(WhenTargetAbsentState); return; }

        if (Vector3.Distance(unit.transform.position, unit.Target.position) < StopDistance)
        {
            unit.MoveToPoint(unit.transform.position);
            ChangeState(WhenGoalAchievedState);
            return;
        }
        unit.RotateToPoint(unit.Target.position);
        unit.MoveToPoint(unit.Target.position);
        //Debug.Log("Soldier target");
    }
    protected override void OnTargetLost(Transform target)
    {
        base.OnTargetLost(target);
        ChangeState(WhenTargetLostState);
    }
    

}

