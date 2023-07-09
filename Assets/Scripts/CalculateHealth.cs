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
    public float MinHealth { get { return _minHealth; } private set { } }
    public float MaxHealth { get { return _maxHealth; } private set { } }

    private void Awake()
    {
        _health = 1;
        _minHealth = 0;
        _maxHealth = 1;
    }
    public void RiseHealth(float heal)
    {
        _health += heal;
    }

    public void DropHealth(float damage)
    {
        _health -= damage;
    }
}
