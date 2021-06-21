using UnityEngine;
public class PursuitUnitState : UnitState
{   
    private void Update()
    {
        if (unit.Target == null)
        {
            unit.MoveToPoint(unit.transform.position);
            return;
        }   
        unit.RotateToPoint(unit.Target.position);
        unit.MoveToPoint(unit.Target.position);
    }
    protected override void OnOut()
    {
        base.OnOut();
        unit.MoveToPoint(unit.transform.position);
    }
}

