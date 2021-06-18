using UnityEngine;
public class PatrulUnitState : UnitState
{
    [Header("States")]
    public UnitState WhenTargetDetectedState;
    public UnitState WhenNoDotsState;

    [Header("Patruling")]
    public float CheckDistance = 1;
    public float PointViewAngle = 20f;
    public Transform[] patrulPoints;

    private int pointsCounter = 0;
   
    public override void OnStateUpdate()   
    {
        base.OnStateUpdate();

        if (patrulPoints == null || unit == null || patrulPoints.Length == 0)
        {
            ChangeState(WhenNoDotsState);
            return;
        }    

        if (Vector3.Distance(patrulPoints[pointsCounter].position, unit.transform.position) < CheckDistance)
            pointsCounter++;

        if (pointsCounter >= patrulPoints.Length) 
            pointsCounter = 0;

        unit.RotateToPoint(patrulPoints[pointsCounter].position);

        Vector3 forward2d = unit.Eye.forward, directionToPoint2d = (patrulPoints[pointsCounter].position - unit.Eye.position).normalized;
        forward2d.y = 0;
        directionToPoint2d.y = 0;

        if (Vector2.Angle(forward2d, directionToPoint2d) < PointViewAngle)
            unit.MoveToPoint(patrulPoints[pointsCounter].position);
        else
            unit.MoveToPoint(unit.transform.position);
    }
    protected override void OnTargetDetected(Transform target)
    {
        base.OnTargetDetected(target);
        ChangeState(WhenTargetDetectedState);
    }
}
