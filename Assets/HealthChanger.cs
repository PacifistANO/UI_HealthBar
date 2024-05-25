using UnityEngine;

public class HealthChanger : MonoBehaviour
{
    [SerializeField] private CharacterHealth _health;

    private float _heal = 10;
    private float _damage = 20;

    public void OnIncreaseClick()
    {
        _health.Increase(_heal);
    }

    public void OnDegreaseClick()
    {
        _health.Degrease(_damage);
    }
}
