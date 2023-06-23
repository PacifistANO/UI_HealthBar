using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System.Globalization;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpChangeCounter;
    [SerializeField] private TMP_Text _hpCount;
    [SerializeField] private Slider _healthBar;

    private CalculateHealth _health;
    private float _delta = 0.2f;
    private CultureInfo _culture = CultureInfo.CurrentCulture.Clone() as CultureInfo;
    private Coroutine _workingHealthCoroutine = null;
    private Coroutine _workingAlphaCoroutine = null;

    private void Start()
    {
        _health = new CalculateHealth();
        _culture.NumberFormat.PercentSymbol = "";
        _healthBar.value = _health.Health;
    }

    public void OnIncreaseClick()
    {
        if (_health.Health < 1)
        {
            _health.RiseHealth();
            _hpChangeCounter.color = Color.green;
            _workingAlphaCoroutine = StartCoroutine(ChangeAlphaCoroutine());
            _workingHealthCoroutine = StartCoroutine(ChangeHealthCoroutine());
        }
    }

    public void OnDegreaseClick()
    {
        if (_health.Health > 0)
        {
            _health.DropHealth();
            _hpChangeCounter.color = Color.red;
            _workingAlphaCoroutine = StartCoroutine(ChangeAlphaCoroutine());
            _workingHealthCoroutine = StartCoroutine(ChangeHealthCoroutine());
        }
    }

    private IEnumerator ChangeHealthCoroutine()
    {
        if(_workingHealthCoroutine != null)
        {
            StopCoroutine(_workingHealthCoroutine);
        }

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
        if (_workingAlphaCoroutine != null)
        {
            StopCoroutine(_workingAlphaCoroutine);
        }

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
