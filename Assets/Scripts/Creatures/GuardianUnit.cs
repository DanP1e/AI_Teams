using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;
using System.Collections.Generic;

public class GuardianUnit : Unit
{
    private Transform _agentTransform;
    private bool _isTargetInArea = false;

    [Header("Team member")]
    [SerializeField] private Teams _teamType;
    [SerializeField] private TeamsCollection _unitTeamsCollection;

    [Header("Detection")]
    [Range(0, 360)]
    [SerializeField] private float _viewAngle = 90f;
    [SerializeField] private float _viewDistance = 15f;
    [SerializeField] private float _detectionDistance = 3f;
    [SerializeField] private Transform _eye;
    [SerializeField] private Transform _target;

    public NavMeshAgent Agent { get; private set; }
    public override Transform Eye => _eye;
    public override Transform Target => _target;
    public override float RotationSpeed => Agent.angularSpeed;
    public override float MovementSpeed => Agent.speed;
    public override int TeamId => (int)_teamType;

    private void Start()
    {
        // Инициализация компонентов
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        _agentTransform = Agent.transform;
        if (_target == null) Debug.LogWarning($"{nameof(_target)} is null!");
    }
    private void OnEnable()
    {
        _unitTeamsCollection.AddUniqueMember(this);
        Dying += OnDiyng;
    }
    private void OnDisable()
    {
        Disqualify();
        Dying -= OnDiyng;      
    }
    private void Update()
    {       
        DrawViewState();
    }

    // Проверяет состояние видимости цели
    public override bool IsTargetInViewArea(Transform target) 
    {
        if (target == null) 
        {
            throw new ArgumentNullException($"{nameof(target)}", $"Should not be empty!");
        }
        float distanceToPlayer = Vector3.Distance(target.transform.position, Eye.transform.position);
        bool result = (distanceToPlayer <= _detectionDistance || IsTargetInEyeArea(target));
        return result;
    }
    // true если цель видна
    private bool IsTargetInEyeArea(Transform target) 
    {
        float realAngle = Vector3.Angle(Eye.forward, target.position - Eye.position);
        RaycastHit hit;
        if (Physics.Raycast(Eye.transform.position, target.position - Eye.position, out hit, _viewDistance)) 
        {
            Debug.DrawLine(Eye.transform.position, hit.point, Color.green);
            if (realAngle < _viewAngle / 2f && Vector3.Distance(Eye.position, target.position) <= _viewDistance && hit.transform == target)
            {
                return true;
            }
        }
        return false;
    }
    private void OnDiyng(IAlive unit)
    {
        Destroy(gameObject);
    }
    // рисует зону видимости
    private void DrawViewState() 
    {
        Vector3 left = Eye.position + Quaternion.Euler(new Vector3(0, _viewAngle / 2f, 0)) * (Eye.forward * _viewDistance);
        Vector3 right = Eye.position + Quaternion.Euler(-new Vector3(0, _viewAngle / 2f, 0)) * (Eye.forward * _viewDistance);
        Debug.DrawLine(Eye.position, left, Color.yellow);
        Debug.DrawLine(Eye.position, right, Color.yellow);
    }
    // поворачивает в сторону цели со скоростью rotationSpeed
    public override void RotateToPoint(Vector3 point)
    {
        Vector3 lookVector = point - _agentTransform.position;
        lookVector.y = 0;
        if (lookVector == Vector3.zero) return;
        _agentTransform.rotation = Quaternion.RotateTowards
            (
                _agentTransform.rotation,
                Quaternion.LookRotation(lookVector, Vector3.up),
                RotationSpeed * Time.deltaTime
            );
    }
    // двигает "солдата" к цели
    public override void MoveToPoint(Vector3 point)
    {
        Debug.DrawLine(Eye.position, point, Color.blue);
        Agent.SetDestination(point);
    }  
    
    public override bool TryFindTarget()
    {
        List<Transform> suitableTransforms = new List<Transform>();
        int countOfMembers = _unitTeamsCollection.CountOfMembers();
        for (int targetId = 0; targetId < countOfMembers; targetId++)
        {
            ITeamMember member = _unitTeamsCollection.GetMemberById(targetId);
          
            if (member.TeamId != TeamId || member.TeamId == 0)
            {
                Component memberComponent = member as Component;
                if (memberComponent == null)
                {
                    throw new ArgumentException("Contains an object not inheriting from UnityEngine.Component", $"{_unitTeamsCollection}");
                }
                if (memberComponent.transform != transform && IsTargetInViewArea(memberComponent.transform))
                {
                    suitableTransforms.Add(memberComponent.transform);
                }                
            }
        }
        if (suitableTransforms.Count != 0)
        {
            _target = FindNearestTransform(suitableTransforms);
            return true;
        }
        return false;
    }

    private Transform FindNearestTransform(List<Transform> transforms)
    {
        int nearestTransformId = 0;
        float nearestDistance = Vector3.Distance(transform.position, transforms[nearestTransformId].position);

        for (int transformId = 1; transformId < transforms.Count; transformId++)
        {
            float currentDistance = Vector3.Distance(transform.position, transforms[transformId].position);
            if (currentDistance < nearestDistance)
            {
                nearestTransformId = transformId;
                nearestDistance = currentDistance;
            }
        }
        return transforms[nearestTransformId];
    }

    
}
