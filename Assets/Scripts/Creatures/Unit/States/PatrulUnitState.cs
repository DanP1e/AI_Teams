using UnityEngine;
public class PatrulUnitState : UnitState
{
    [Header("Patruling")]
    public float CheckDistance = 1;
    public Transform[] patrulPoints;

    private int _pointsCounter = 0;

    private void OnEnable()
    {
        if (patrulPoints.Length == 0)
        {
            Debug.LogError($"{nameof(patrulPoints)} are not set!");
        }
    }

    private void Update()
    {
        if (Vector3.Distance(patrulPoints[_pointsCounter].position, unit.transform.position) < CheckDistance)
            _pointsCounter++;

        if (_pointsCounter >= patrulPoints.Length)
            _pointsCounter = 0;

        unit.RotateToPoint(patrulPoints[_pointsCounter].position);
        unit.MoveToPoint(patrulPoints[_pointsCounter].position);       
    }
}
