using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AliveTeamMember : Entity, ITeamMember
{    
    public abstract int TeamId { get; }

    public event Action<ITeamMember> Disqualified;


    // ВЫЗЫВАТЬ КОГДА ОБЪЕКТ БОЛЬШЕ НЕ НУЖЕН КАК ЧЛЕН КОМАНДЫ
    public void Disqualify()
    {
        OnDisqualify();
        Disqualified?.Invoke(this);
    }

    protected virtual void OnDisqualify() { }
}
