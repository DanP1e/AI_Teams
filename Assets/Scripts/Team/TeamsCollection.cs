using System;
using System.Collections.Generic;
using UnityEngine;

public class TeamsCollection : MonoBehaviour
{
    private List<ITeamMember> _teamMembers = new List<ITeamMember>();
    public ITeamMember GetMemberById(int id) => _teamMembers[id];
    public int CountOfMembers() => _teamMembers.Count;

    // Добавляет нового уникального члена команды в список
    public void AddUniqueMember(AliveTeamMember teamMember)
    {
        if (_teamMembers.Contains(teamMember))
        {
            throw new ArgumentException("This member is already on the list!", nameof(teamMember));
        }
        teamMember.Disqualified += RemoveMember;
        _teamMembers.Add(teamMember);
    }

    // Удаляет члена команды из списка
    public void RemoveMember(ITeamMember teamMember)
    {
        if (!_teamMembers.Contains(teamMember))
        {
            throw new ArgumentException("This member is not in the list!", nameof(teamMember));
        }
        teamMember.Disqualified -= RemoveMember;
        _teamMembers.Remove(teamMember);
    }
}
