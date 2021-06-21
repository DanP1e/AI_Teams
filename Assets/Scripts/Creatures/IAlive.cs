using System;
using UnityEngine.Events;

public interface IAlive
{
    event UnityAction<IAlive> Dying;

    float HP { get; }
    float MaxHP { get; }

    float Heal(float addedHealth);
    void MakeDamage(float stolenHealth);
}

