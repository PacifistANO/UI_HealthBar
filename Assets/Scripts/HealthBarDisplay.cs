using UnityEngine;
using UnityEngine.UI;

public class HealthBarDisplay : HealthDisplay
{
    [SerializeField] private Slider _healthBar;

    private void Start()
    {
        _healthBar.maxValue = Health.MaxValue;
        _healthBar.value = Health.Value;
    }

    public override void OnIncreaseClick()
    {
        _healthBar.value = Health.Value;
    }

    public override void OnDegreaseClick()
    {
        _healthBar.value = Health.Value;
    }
}
