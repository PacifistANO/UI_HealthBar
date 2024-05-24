using UnityEngine;

[RequireComponent(typeof(CharacterHealth))]
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private CharacterHealth _health;

    private float _heal = 10;
    private float _damage = 20;

    protected CharacterHealth Health => _health;
    protected float Heal => _heal;
    protected float Damage => _damage;

    private void OnEnable()
    {
        _health = GetComponent<CharacterHealth>();
    }

    public virtual void OnIncreaseClick()
    {
        _health.Increase(_heal);
    }

    public virtual void OnDegreaseClick()
    {
        _health.Decrease(_damage);
    }

    protected float ChangeValue(float currentValue, float finalValue, float delta = 50)
    {
        currentValue = Mathf.MoveTowards(currentValue, finalValue, delta * Time.deltaTime);
        return currentValue;
    }
}
