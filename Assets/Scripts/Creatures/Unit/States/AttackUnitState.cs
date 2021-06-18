using UnityEngine;
public class AttackUnitState : UnitState
{
    [Header("States")]
    public UnitState WhenAttackAuraExitState;
    public UnitState WhenTargetDestroyedState;

    [Header("Attack preferences")]
    public float AttackAuraRange;
    public float Damage;
    public float Cooldown;

    private IAlive targetHealth;
    private float timer = 0;
    protected override void Init()
    {
        base.Init();
        targetHealth = GetTargetHealth();
        if (targetHealth == null)
            ChangeState(WhenTargetDestroyedState);
    }
    protected override void Final()
    {
        base.Final();
    }
    private IAlive GetTargetHealth() 
    {
        if (unit == null) { Debug.LogError($"The {typeof(Unit).Name} class is null"); return null; }
        if (unit.Target == null) { Debug.LogWarning($"The target do not assigned in {typeof(Unit).Name} class"); return null; }

        return unit.Target.GetHeir<IAlive>();
    }
   
    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
        Transform unitTransform = unit.transform;
        Transform unitTarget = unit.Target;
        if (unitTarget == null) 
        {
            ChangeState(WhenTargetDestroyedState);
            return;
        }           
        unit.RotateToPoint(unitTarget.position);
        if (Vector3.Distance(unitTransform.position, unitTarget.position) < AttackAuraRange && targetHealth != null)
        {
            if (timer <= 0)
            {
                timer = Cooldown;
                targetHealth.MakeDamage(Damage);
                Debug.DrawLine(unitTarget.position, unitTransform.position, Color.red);
            }
            timer -= Time.deltaTime;
        }
        else
        {
            ChangeState(WhenAttackAuraExitState);
        }
    }
           
}
