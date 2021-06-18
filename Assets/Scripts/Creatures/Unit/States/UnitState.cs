using UnityEngine;
using System;
public abstract class UnitState : State
{
    private bool _isTargetInViewOld = false;
    private Transform oldTargetTransform;

    [SerializeField] protected Unit unit = null;

    public override void OnStateUpdate()
    {     
        if (!unit.TryFindTarget())
        {
            return;
        }

        Transform unitTargetTransfom = unit.Target;
        bool isTargetInViewNow = unit.IsTargetInViewArea(unitTargetTransfom);

        if (_isTargetInViewOld != isTargetInViewNow)
        {
            if (isTargetInViewNow)
            {
                OnTargetDetected(unitTargetTransfom);
            }
            else
            {
                OnTargetLost(unitTargetTransfom);
            }
            _isTargetInViewOld = isTargetInViewNow;
        }
        else if (oldTargetTransform != unitTargetTransfom)
        {
            OnTargetChanged(unitTargetTransfom);
        }
        oldTargetTransform = unitTargetTransfom;
    }

    // Вызывается при входе в событие
    protected override void Init() 
    { 
        base.Init();
        Transform unitTargetTransform = unit.Target;
        oldTargetTransform = unitTargetTransform;     
    }

    // Вызывается при выходе из события
    protected override void Final()
    {
        base.Final();
    }
    protected virtual void OnTargetChanged(Transform target) { }
    protected virtual void OnTargetLost(Transform target) { }
    protected virtual void OnTargetDetected(Transform target) { }
}

