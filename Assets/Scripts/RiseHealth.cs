using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System.Globalization;

public class RiseHealth : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpChangeCounter;
    [SerializeField] private TMP_Text _hpCount;
    [SerializeField] private Slider _healthBar;

    private float _health = 1;
    private float _damage = -0.1f;
    private float _heal = 0.1f;
    private float _delta = 0.2f;
    private CultureInfo _culture = CultureInfo.CurrentCulture.Clone() as CultureInfo;
    private Coroutine _workingHealthCoroutine = null;
    private Coroutine _workingAlphaCoroutine = null;

    private void Start()
    {
        _culture.NumberFormat.PercentSymbol = "";
        _healthBar.value = _health;
    }

    public void OnIncreaseClick()
    {
        if (_health < 1)
        {
            _hpChangeCounter.color = Color.green;
            _workingAlphaCoroutine = StartCoroutine(ChangeAlphaCoroutine());
            _workingHealthCoroutine = StartCoroutine(ChangeHealthCoroutine(_heal));
        }
    }

    public void OnDegreaseClick()
    {
        if (_health > 0)
        {
            _hpChangeCounter.color = Color.red;
            _workingAlphaCoroutine = StartCoroutine(ChangeAlphaCoroutine());
            _workingHealthCoroutine = StartCoroutine(ChangeHealthCoroutine(_damage));
        }
    }

    private IEnumerator ChangeHealthCoroutine(float deltaHealth)
    {
        if(_workingHealthCoroutine != null)
        {
            StopCoroutine(_workingHealthCoroutine);
        }

        _health += deltaHealth;
        _hpCount.text = _health.ToString("P0", _culture);

        while (_healthBar.value != _health)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, _health, _delta * Time.deltaTime);

            if (_healthBar.value == _health)
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
