using System;
public interface ITeamMember
{   
    event Action<ITeamMember> Disqualified;

    int TeamId { get; }

    void Disqualify();
}
