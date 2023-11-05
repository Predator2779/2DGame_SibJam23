using System;
using Scripts.Character.Classes;
using UnityEngine;

[RequireComponent(typeof(Boss))]
public class BossAI : CharacterAI
{
    [SerializeField] private Transform enemy;

    [SerializeField] private Trigger meleeTrigger;

    [SerializeField] private float meleeDuration;
    [SerializeField] private float meleeCooldown;
    private float _meleeTimestamp = float.MinValue;
    private bool _isInMeleeRange = false;

    private BossAIState _state = BossAIState.Armed;
    private float _stateTimestamp = 0f;

    private Boss _boss;

    protected override void Awake()
    {
        _boss = GetComponent<Boss>();

        meleeTrigger._triggerEnterCallback = (_, c) =>
        {
            if (c.transform == enemy)
            {
                _isInMeleeRange = true;
            }
        };
        meleeTrigger._triggerExitCallback = (_, c) =>
        {
            if (c.transform == enemy)
            {
                _isInMeleeRange = false;
            }
        };
    }

    protected override void Update()
    {
        switch (_state)
        {
            case BossAIState.Idle:
                break;
            case BossAIState.Armed:
                _boss.MoveTo((Vector2.right * (enemy.position.x - meleeTrigger.transform.position.x)).normalized);

                if (_isInMeleeRange && Time.time - _meleeTimestamp > meleeDuration + meleeCooldown)
                {
                    _boss.UsePrimaryAction();
                    _state = BossAIState.MeleeAttack;
                    _meleeTimestamp = Time.time;
                }

                break;
            case BossAIState.Disarmed:
                break;
            case BossAIState.MeleeAttack:
                if (Time.time - _meleeTimestamp > meleeDuration)
                {
                    _state = BossAIState.Armed;
                }

                break;
            case BossAIState.RangedAttack:
                break;
            case BossAIState.AoeAttack:
                break;
            case BossAIState.AoeStunned:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Activate()
    {
        if (_state == BossAIState.Idle)
        {
            _state = _boss.holdedItem != null ? BossAIState.Armed : BossAIState.Disarmed;
        }
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