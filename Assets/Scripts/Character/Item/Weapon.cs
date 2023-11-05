using UnityEngine;

public class Weapon : UsableItem
{
    [SerializeField] private int _damage;
    private int _damageFactor = 1;

    public int Damage => _damage * _damageFactor;
    public int DamageFactor { set => _damageFactor = value; }
}