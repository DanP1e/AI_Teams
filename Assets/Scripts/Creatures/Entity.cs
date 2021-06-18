using UnityEngine;
using System;

public abstract class Entity : MonoBehaviour, IAlive
{
    [SerializeField] private float _hp;
    [SerializeField] private float _maxHP;

    public float HP => _hp;
    public float MaxHP => _maxHP;

    public event Action<IAlive> Dying;

    public float Heal(float addedHp)
    {       
        _hp += addedHp;
        if (_hp > _maxHP)
        {
            float difference = _maxHP - _hp;
            _hp = _maxHP;
            return difference;
        }
        CheckAliveState();
        return 0;
    }

    public void MakeDamage(float retrievedHp)
    {
        _hp -= retrievedHp;
        CheckAliveState();
    }

    private void CheckAliveState() 
    {
        if (_hp <= 0)
        {
            Dying?.Invoke(this);
        }
    }
}

