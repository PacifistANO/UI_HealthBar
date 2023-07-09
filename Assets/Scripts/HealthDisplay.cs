using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System.Globalization;
using Unity.VisualScripting;

[RequireComponent(typeof(CalculateHealth))]

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpChangeCounter;
    [SerializeField] private TMP_Text _hpCount;
    [SerializeField] private Slider _healthBar;

    private CalculateHealth _health;
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
        _health.RiseHealth(_heal);
        _hpChangeCounter.color = Color.green;
        ChangeHealthBar();
    }

    public void OnDegreaseClick()
    {
        _health.DropHealth(_damage);
        _hpChangeCounter.color = Color.red;
        ChangeHealthBar();
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

    private void ChangeValue(ref float currentValue, float finalValue, float delta, ref Coroutine workingCoroutine)
    {
        currentValue = Mathf.MoveTowards(currentValue, finalValue, delta * Time.deltaTime);

        if (currentValue == finalValue)
        {
            StopCoroutine(workingCoroutine);
        }
    }

    private IEnumerator ChangeAlphaCoroutine()
    {
        float alphaValue = _hpChangeCounter.alpha;

        while (_hpChangeCounter.alpha != 0)
        {
            ChangeValue(ref alphaValue, 0f, 1f, ref _workingAlphaCoroutine);
            _hpChangeCounter.alpha = alphaValue;

            yield return null;
        }
    }

    private IEnumerator ChangeHealthCoroutine()
    {
        _hpCount.text = _health.Health.ToString("P0", _culture);
        float healthValue = _healthBar.value;

        while (_healthBar.value != _health.Health)
        {
            ChangeValue(ref healthValue, _health.Health, 0.2f, ref _workingHealthCoroutine);
            _healthBar.value = healthValue;

            yield return null;
        }
    }
}
