using System;
using Character.Item;
using Scripts.Character.Classes;
using UnityEngine;

[RequireComponent(typeof(Boss))]
public class BossAI : CharacterAI
{
    [SerializeField] private Trigger meleeTrigger;
    [SerializeField] private float meleeDuration;
    [SerializeField] private float meleeCooldown;
    [SerializeField] private Trigger rangedTrigger;
    [SerializeField] private float rangedDuration;
    [SerializeField] private float rangedCooldown;

    public bool _isTest;
    
    private float _meleeTimestamp = float.MinValue;
    private bool _isInMeleeRange;

    private float _rangedTimestamp = float.MinValue;
    private bool _isInRange;

    private BossAIState _state = BossAIState.Armed;
    private float _stateTimestamp = 0f;

    private Boss _boss;
    [SerializeField] private Transform _enemy;

    private ThrownWeapon _thrownWeapon;

    protected override void Awake()
    {
        _boss = GetComponent<Boss>();

        meleeTrigger._triggerEnterCallback = (_, c) =>
        {
            if (c.transform.CompareTag("Player"))
            {
                _enemy = c.transform;
                _isInMeleeRange = true;
            }
        };
        meleeTrigger._triggerExitCallback = (_, c) =>
        {
            if (c.transform.CompareTag("Player"))
            {
                _isInMeleeRange = false;
            }
        };

        rangedTrigger._triggerEnterCallback = (_, c) =>
        {
            if (c.transform.CompareTag("Player"))
            {
                _enemy = c.transform;
                _isInRange = true;
            }
        };
        rangedTrigger._triggerExitCallback = (_, c) =>
        {
            if (c.transform.CompareTag("Player"))
            {
                _isInRange = false;
            }
        };
    }

    protected override void Update()
    {
        if (_isTest)
        {
            _boss.Death();
        }
        
        Debug.Log(_state);
        switch (_state)
        {
            case BossAIState.Idle:
                break;
            case BossAIState.Armed:

                if (_enemy == null) return;

                Vector2 moveDirection = GetMoveDirection();

                _boss.MoveTo(moveDirection);
                CheckSpriteSide();

                if (_boss.heldWeapon.gameObject.activeSelf && _isInRange &&
                    Time.time - _rangedTimestamp > rangedDuration + rangedCooldown)
                {
                    _thrownWeapon = _boss.RangedAttack();
                    _state = BossAIState.RangedAttack;
                    _rangedTimestamp = Time.time;
                }

                if (_isInMeleeRange && Time.time - _meleeTimestamp > meleeDuration + meleeCooldown)
                {
                    _boss.MeleeAttack();
                    _state = BossAIState.MeleeAttack;
                    _meleeTimestamp = Time.time;
                }

                break;
            case BossAIState.Disarmed:
                if (_thrownWeapon != null)
                {
                    var direction = (_thrownWeapon.transform.position.x - transform.position.x);
                    _boss.MoveTo(Math.Abs(direction) > 1
                        ? (direction * Vector2.right).normalized
                        : direction * Vector2.right);
                }
                else
                {
                    _state = BossAIState.Armed;
                }

                break;
            case BossAIState.MeleeAttack:
                _boss.StopMove();
                if (Time.time - _meleeTimestamp > meleeDuration)
                {
                    _state = BossAIState.Armed;
                }

                break;
            case BossAIState.RangedAttack:
                _boss.StopMove();
                if (Time.time - _rangedTimestamp > rangedDuration)
                {
                    _state = BossAIState.Disarmed;
                }

                break;
            case BossAIState.AoeAttack:
                break;
            case BossAIState.AoeStunned:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_state == BossAIState.Disarmed && _thrownWeapon != null && col.gameObject == _thrownWeapon.gameObject)
        {
            Destroy(_thrownWeapon.gameObject);
            _thrownWeapon = null;

            _boss.heldWeapon.gameObject.SetActive(true);
        }
    }

    public void Activate()
    {
        if (_state == BossAIState.Idle)
        {
            _state = _boss.heldWeapon.gameObject.activeSelf ? BossAIState.Armed : BossAIState.Disarmed;
        }
    }

    private Vector2 GetMoveDirection()
    {
        var target = (rangedDuration + rangedCooldown) - (Time.time - _rangedTimestamp) < 2
            ? rangedTrigger.transform.position.x
            : meleeTrigger.transform.position.x;
        var direction = (Vector2.right * (_enemy.position.x - target));
        return direction.sqrMagnitude > 1 ? direction.normalized : direction;
    }

    private void CheckSpriteSide()
    {
        _boss.SetSpriteSide(_enemy.transform.position.x > transform.position.x
            ? TurnHandler.playerSides.Right
            : TurnHandler.playerSides.Left);
    }

    private enum BossAIState
    {
        Idle,
        Armed,
        Disarmed,
        MeleeAttack,
        RangedAttack,
        AoeAttack,
        AoeStunned
    }
}