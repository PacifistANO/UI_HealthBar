using System.Collections;
using TMPro;
using UnityEngine;

public class TextHealthDisplay : HealthDisplay
{
    [SerializeField] private TMP_Text _hpCount;
    [SerializeField] private TMP_Text _hpChangeCounter;

    private void Start()
    {
        _hpCount.text = (Health.Value + "/" + Health.MaxValue);
    }

    public override void OnIncreaseClick()
    {
        _hpChangeCounter.text = Heal.ToString();
        _hpChangeCounter.color = Color.green;
        _hpCount.text = (Health.Value + "/" + Health.MaxValue);
        StartCoroutine(ChangeAlphaCoroutine());
    }

    public override void OnDegreaseClick()
    {
        _hpChangeCounter.text = Damage.ToString();
        _hpChangeCounter.color = Color.red;
        _hpCount.text = (Health.Value + "/" + Health.MaxValue);
        StartCoroutine(ChangeAlphaCoroutine());
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
}
