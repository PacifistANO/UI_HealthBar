using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthDisplay : HealthDisplay
{
    [SerializeField] private Slider _smoothHealthBar;

    private void Start()
    {
        _smoothHealthBar.maxValue = Health.Value;
        _smoothHealthBar.value = Health.Value;
    }

    public override void OnIncreaseClick()
    {
        StartCoroutine(ChangeHealthBar());
    }

    public override void OnDegreaseClick()
    {
        StartCoroutine(ChangeHealthBar());
    }

    private IEnumerator ChangeHealthBar()
    {
        float healthValue = _smoothHealthBar.value;

        while (_smoothHealthBar.value != Health.Value)
        {
            healthValue = ChangeValue(healthValue, Health.Value);
            _smoothHealthBar.value = healthValue;

            yield return null;
        }
    }
}
