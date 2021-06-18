using System;

public interface IAlive
{
    event Action<IAlive> Dying;

    float HP { get; }
    float MaxHP { get; }

    float Heal(float addedHp);
    void MakeDamage(float retrievedHp);
}

