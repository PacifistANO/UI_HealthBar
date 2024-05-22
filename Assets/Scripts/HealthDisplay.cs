using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterHealth))]
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpChangeCounter;
    [SerializeField] private TMP_Text _hpCount;
    [SerializeField] private Slider _smoothHealthBar;
    [SerializeField] private Slider _healthBar;

    private CharacterHealth _health;
    private float _heal = 10;
    private float _damage = 20;

    private void Start()
    {
        _health = GetComponent<CharacterHealth>();
        _smoothHealthBar.maxValue = _health.Value;
        _smoothHealthBar.value = _smoothHealthBar.maxValue;
        _healthBar.maxValue = _smoothHealthBar.maxValue;
        _healthBar.value = _smoothHealthBar.value;
        _hpCount.text = (_health.Value + "/" + _health.MaxValue);
    }

    public void OnIncreaseClick()
    {
        _hpChangeCounter.text = _heal.ToString();
        _health.IncreaseHealth(_heal);
        _hpChangeCounter.color = Color.green;
        ChangeHealthBar();
    }

    public void OnDegreaseClick()
    {
        _hpChangeCounter.text = _damage.ToString();
        _health.DecreaseHealth(_damage);
        _hpChangeCounter.color = Color.red;
        ChangeHealthBar();
    }

    private void ChangeHealthBar()
    {
        StartCoroutine(ChangeHealthCoroutine());
        StartCoroutine(ChangeAlphaCoroutine());
    }

    private float ChangeValue(float currentValue, float finalValue, float delta = 50)
    {
        currentValue = Mathf.MoveTowards(currentValue, finalValue, delta * Time.deltaTime);
        return currentValue;
    }

    private IEnumerator ChangeAlphaCoroutine()
    {
        float alphaValue = _hpChangeCounter.alpha;

        while (_hpChangeCounter.alpha != 0)
        {
            alphaValue = ChangeValue(alphaValue, 0, 3);
            _hpChangeCounter.alpha = alphaValue;

            yield return null;
        }
    }

    private IEnumerator ChangeHealthCoroutine()
    {
        _hpCount.text = (_health.Value + "/" + _health.MaxValue);
        float healthValue = _smoothHealthBar.value;
        _healthBar.value = _health.Value;

        while (_smoothHealthBar.value != _health.Value)
        {
            healthValue = ChangeValue(healthValue, _health.Value);
            _smoothHealthBar.value = healthValue;

            yield return null;
        }
    }
}
