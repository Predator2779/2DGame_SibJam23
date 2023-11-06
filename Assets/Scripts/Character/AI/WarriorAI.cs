using Scripts.Character.Classes;
using UnityEngine;

[RequireComponent(typeof(Warrior))]
public class WarriorAI : CharacterAI
{
    private Transform _enemy;
    protected Warrior _warrior;

    private bool _isInMeleeRange;

    protected override void Awake()
    {
        _enemy = GameObject.FindWithTag("Player").transform;
        _warrior = GetComponent<Warrior>();
    }

    protected override void Update()
    {
        base.Update();
        
        _warrior.RotateByAngle(_warrior.transform,
            _enemy.transform.position.x > transform.position.x ? 0f : 180f);

        CheckEnemy();
    }

    private void CheckEnemy()
    {
        if (_isInMeleeRange && _enemy.TryGetComponent(out HealthProcessor _))
            _warrior.Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _isInMeleeRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _isInMeleeRange = false;
    }
}