using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public abstract class Unit : AliveTeamMember
{
    public abstract Transform Eye { get; }
    public abstract Transform Target { get; }
    public abstract float RotationSpeed { get; }   
    public abstract float MovementSpeed { get; }

    // поворачивает unit в сторону точки на шаг * RotationSpeed
    public abstract void RotateToPoint(Vector3 point);

    // двигает unit к точке на шаг * MovementSpeed
    public abstract void MoveToPoint(Vector3 point);

    // Проверяет видна ли точка из "глаза" юнита
    public abstract bool IsTransformInViewArea(Transform transform);
}
