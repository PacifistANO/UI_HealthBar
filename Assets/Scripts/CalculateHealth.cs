using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CalculateHealth: MonoBehaviour 
{
    private float _health;
    private int _maxHealth;
    private int _minHealth;

    public float Health { get { return _health; } private set { } }

    private void Awake()
    {
        _health = 1;
        _minHealth = 0;
        _maxHealth = 1;
    }

    public void RiseHealth(float heal)
    {
        if (_health < _maxHealth)
        {
            _health += heal;

            if (_health >= _maxHealth)
            {
                _health = _maxHealth;
            }
        }
    }

    public void DropHealth(float damage)
    {
        if (_health > _minHealth)
        {
            _health -= damage;

            if (_health <= _minHealth)
            {
                _health = _minHealth;
            }
        }
    }
}
