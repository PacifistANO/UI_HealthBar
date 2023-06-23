using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateHealth
{
    private float _health;
    private float _impact;

    public float Health { get { return _health; } private set { } }

    public CalculateHealth() 
    {
        _health = 1;
        _impact = 0.1f;
    }

    public void RiseHealth()
    {
        _health += _impact;
    }

    public void DropHealth()
    {
        _health -= _impact;
    }
}
