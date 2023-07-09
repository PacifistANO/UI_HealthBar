using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System.Globalization;

[RequireComponent(typeof(CalculateHealth))]

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpChangeCounter;
    [SerializeField] private TMP_Text _hpCount;
    [SerializeField] private Slider _healthBar;

    private CalculateHealth _health;
    private float _delta = 0.2f;
    private float _heal = 0.1f;
    private float _damage = 0.1f;
    private CultureInfo _culture = CultureInfo.CurrentCulture.Clone() as CultureInfo;
    private Coroutine _workingHealthCoroutine = null;
    private Coroutine _workingAlphaCoroutine = null;

    private void Start()
    {
        _health = GetComponent<CalculateHealth>();
        _culture.NumberFormat.PercentSymbol = "";
        _healthBar.value = _health.Health;
    }

    public void OnIncreaseClick()
    {
        if (_health.Health < _health.MaxHealth)
        {
            _health.RiseHealth(_heal);
            ChangeHealthBar();
            _hpChangeCounter.color = Color.green;
        }
    }

    public void OnDegreaseClick()
    {
        if( _health.Health > _health.MinHealth) 
        {
            _health.DropHealth(_damage);
            ChangeHealthBar();
            _hpChangeCounter.color = Color.red;
        }
    }

    private void ChangeHealthBar()
    {
        if (_workingAlphaCoroutine != null && _workingHealthCoroutine != null)
        {
            StopCoroutine(_workingHealthCoroutine);
            StopCoroutine(_workingAlphaCoroutine);
        }

        _workingHealthCoroutine = StartCoroutine(ChangeHealthCoroutine());
        _workingAlphaCoroutine = StartCoroutine(ChangeAlphaCoroutine());
    }

    private IEnumerator ChangeHealthCoroutine()
    {
        _hpCount.text = _health.Health.ToString("P0", _culture);

        while (_healthBar.value != _health.Health)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, _health.Health, _delta * Time.deltaTime);

            if (_healthBar.value == _health.Health)
            {
                StopCoroutine(_workingHealthCoroutine);
            }

            yield return null;
        }
    }

    private IEnumerator ChangeAlphaCoroutine()
    {
        _hpChangeCounter.alpha = 1f;

        while (_hpChangeCounter.alpha != 0)
        {
            _hpChangeCounter.alpha = Mathf.MoveTowards(_hpChangeCounter.alpha, 0, Time.deltaTime);

            if (_hpChangeCounter.alpha == 0)
            {
                StopCoroutine(ChangeAlphaCoroutine());
            }

            yield return null;
        }
    }
}
