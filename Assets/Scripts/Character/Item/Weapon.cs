using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] protected bool _destroyAfterUsing;
    [SerializeField] private float _delayUsing = 0.1f;

    public bool undamagedWeapon;
    public int Damage => _damage * _damageFactor;
    private int _damageFactor = 1;
    private bool _canUse = true;

    public int DamageFactor { set => _damageFactor = value; }
    
    protected virtual void Start() => _canUse = true;

    public void TakeDamage(List<HealthProcessor> healthProcessors)
    {
        if (!_canUse || !enabled || undamagedWeapon) return;

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