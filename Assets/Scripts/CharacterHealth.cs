using TMPro;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float _value;

    private float _maxValue;

    public float Value => _value;
    public float MaxValue => _maxValue;

    private void Awake()
    {
        _maxValue = _value;
    }

    public void IncreaseHealth(float addition)
    {
        if (addition >= 0)
            if (_value + addition < _maxValue)
                _value += addition;
            else
                _value = _maxValue;
    }

    public void DecreaseHealth(float reduction)
    {
        if (reduction >= 0)
            if (_value - reduction > 0)
                _value -= reduction;
            else
                _value = 0;
    }
}
