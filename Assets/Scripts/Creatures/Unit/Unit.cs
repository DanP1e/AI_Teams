using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Unit : AliveTeamMember
{
    public abstract Transform Eye { get; }
    public abstract Transform Target { get; }
    public abstract float RotationSpeed { get; }   
    public abstract float MovementSpeed { get; }

    // поворачивает в сторону цели на шаг со скоростью RotationSpeed
    public abstract void RotateToPoint(Vector3 point);

    // перемещает "юнит" на шаг к точке
    public abstract void MoveToPoint(Vector3 point);

    // Проверяет видна ли точка из "глаза" юнита
    public abstract bool IsTargetInViewArea(Transform transform);

    // Попытаться найти подходящую цель
    public abstract bool TryFindTarget();
}
