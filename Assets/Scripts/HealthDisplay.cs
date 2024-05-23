using UnityEngine;

[RequireComponent(typeof(CharacterHealth))]
public class HealthDisplay : MonoBehaviour
{
    protected CharacterHealth Health;
    protected float Heal = 10;
    protected float Damage = 20;

    private void OnEnable()
    {
        Health = GetComponent<CharacterHealth>();
    }

    public virtual void OnIncreaseClick()
    {
        Health.Increase(Heal);
    }

    public virtual void OnDegreaseClick()
    {
        Health.Decrease(Damage);
    }

    protected float ChangeValue(float currentValue, float finalValue, float delta = 50)
    {
        currentValue = Mathf.MoveTowards(currentValue, finalValue, delta * Time.deltaTime);
        return currentValue;
    }
}
