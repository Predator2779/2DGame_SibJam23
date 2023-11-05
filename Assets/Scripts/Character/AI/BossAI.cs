using System;
using Scripts.Character.Classes;
using UnityEngine;

[RequireComponent(typeof(Warrior))]
public class BossAI : CharacterAI
{
    [SerializeField] private Trigger meleeTrigger;
    [SerializeField] private float meleeDuration;
    [SerializeField] private float meleeCooldown;

    private float _meleeTimestamp = float.MinValue;
    private bool _isInMeleeRange = false;

    private BossAIState _state = BossAIState.Armed;
    private float _stateTimestamp = 0f;

    private Warrior _boss;
    private Transform _enemy;

    protected override void Awake()
    {
        _boss = GetComponent<Warrior>();
        
        meleeTrigger._triggerEnterCallback = (_, c) =>
        {
            if (c.CompareTag("Player"))
            {
                _enemy = c.transform;
                _isInMeleeRange = true;
            }
        };
        meleeTrigger._triggerExitCallback = (_, c) =>
        {
            if (c.CompareTag("Player"))
            {
                _enemy = null;
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
                if (_enemy == null) return;

                Vector2 moveDirection = GetMoveDirection();

                _boss.MoveTo(moveDirection);
                CheckSpriteSide(moveDirection);
                
                if (_isInMeleeRange && Time.time - _meleeTimestamp > meleeDuration + meleeCooldown)
                {
                    _boss.Attack();
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
            _state = _boss.currentWeapon != null ? BossAIState.Armed : BossAIState.Disarmed;
        }
    }

    private Vector2 GetMoveDirection() => (Vector2.right * (_enemy.position.x - meleeTrigger.transform.position.x)).normalized;
    
    private void CheckSpriteSide(Vector2 direction)
    {
        if (direction.x < 0) _boss.SetSpriteSide(TurnHandler.playerSides.Left);

        if (direction.x > 0) _boss.SetSpriteSide(TurnHandler.playerSides.Right);
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