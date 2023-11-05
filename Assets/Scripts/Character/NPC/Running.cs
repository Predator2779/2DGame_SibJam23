using System.Collections;
using Scripts.Character.Classes;
using UnityEngine;

[RequireComponent(typeof(Person))]
public class Running : MonoBehaviour
{
    [SerializeField] private float _timeRunning;
    [SerializeField] private Reflections _reflections;
    [SerializeField] private string _runningText;

    private Vector2 _runDirection;
    private bool _isRunning;
    private Person _person;
    [SerializeField] private Warrior _enemy;

    private void OnValidate() => SetNullableFields();

    private void Start() => SetNullableFields();

    private void SetNullableFields()
    {
        _person ??= GetComponent<Person>();
    }

    private void Update() => Run();

    private void Run()
    {
        if (!_isRunning)
        {
            _person.StopMove();
            _reflections.Say(false, "");
            return;
        }
        
        _person.MoveTo(_runDirection);
        _reflections.Say(true, _runningText);
    }

    private void SetMoveDirection()
    {
        int rand = Random.Range(0, 10);
        int coef = 1;

        if (rand > 8) coef = -1;
        
        _runDirection = (coef * Vector2.right * (_enemy.transform.position.x - transform.position.x)).normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && 
            other.TryGetComponent(out Warrior warrior) && 
            !warrior.currentWeapon.undamagedWeapon &&
            !_isRunning)
        {
            _enemy = warrior;
            StartCoroutine(CanRun());
        }
    }

    private IEnumerator CanRun()
    {
        SetMoveDirection();
        _isRunning = true;
        _person.Jump();
        yield return new WaitForSeconds(_timeRunning);
        _isRunning = false;
    }
}