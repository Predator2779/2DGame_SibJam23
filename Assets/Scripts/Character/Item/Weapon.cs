using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    private int _damageFactor = 1;

    public int Damage => _damage * _damageFactor;

    public int DamageFactor
    {
        set => _damageFactor = value;
    }

    public string nameItem;

    [SerializeField] protected bool _destroyAfterUsing;
    [SerializeField] protected float _delayUsing = 0.1f;

    protected bool _canUse = true;

    private void Start() => _canUse = true;

    public void TakeDamage(List<HealthProcessor> healthProcessors)
    {
        if (!_canUse || !enabled) return;

        foreach (var responsable in healthProcessors)
            responsable.ResponseAction(gameObject);

        StartCoroutine(CanUse());

        if (_destroyAfterUsing)
            Destroy(gameObject);
    }

    private IEnumerator CanUse()
    {
        _canUse = false;
        yield return new WaitForSeconds(_delayUsing);
        _canUse = true;
    }
}