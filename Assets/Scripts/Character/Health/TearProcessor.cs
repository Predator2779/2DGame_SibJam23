using Character.Item;
using Core.Health;
using UnityEngine;

public class TearProcessor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _maxHitPoints;
    [SerializeField] private int _currentHitPoints;
    [SerializeField] [Min(1)] private float _coefDefense;
    
    private Health _health;

    private void Start() => Initialize();

    private void Initialize()
    {
        _health = new Health(_maxHitPoints, _coefDefense, _currentHitPoints);
        ChangeHealthBar();
    }
    
    public void ResponseAction(Tear tear)
    {
        TakeTear(tear.TearPoints);
    }
    
    private void TakeTear(float tears)
    {
        if (!CanHealing())
            return;

        _health.TakeHeal(tears);
        ChangeHealthBar();
    }

    public void ChangeHealthBar()
    {
        _currentHitPoints = _health.HitPoints;

        if (_healthBar != null)
            _healthBar.SetCurrentHealth(_currentHitPoints * 100 / _maxHitPoints);
    }

    private bool CanHealing() => _currentHitPoints < _maxHitPoints;
}