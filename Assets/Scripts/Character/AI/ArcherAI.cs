using System;
using Scripts.Character.Classes;
using UnityEngine;

namespace Character.AI
{
    [RequireComponent(typeof(Archer))]
    public class ArcherAI : CharacterAI
    {
        [SerializeField] private Trigger attackTrigger;
        private bool _isInRange = false;

        [SerializeField] private float attackDuration = 1;
        [SerializeField] private float attackCooldown = 3;
        private float _attackTimestamp = float.MinValue;

        private ArcherAIState _state = ArcherAIState.Idle;

        private Archer _archer;
        private GameObject _enemy;

        protected override void Awake()
        {
            _archer = GetComponent<Archer>();
            _enemy = GameObject.FindWithTag("Player");

            attackTrigger._triggerEnterCallback = (_, c) =>
            {
                if (c.CompareTag("Player"))
                {
                    _isInRange = true;
                }
            };
            attackTrigger._triggerExitCallback = (_, c) =>
            {
                if (c.CompareTag("Player"))
                {
                    _isInRange = false;
                }
            };
        }

        protected override void Update()
        {
            switch (_state)
            {
                case ArcherAIState.Idle:
                    if (_enemy != null)
                        _archer.RotateByAngle(_archer.transform,
                                _enemy.transform.position.x > transform.position.x ? 0f : 180f);

                    if (_isInRange && Time.time - _attackTimestamp > attackDuration + attackCooldown)
                    {
                        _archer.Shoot();
                        _attackTimestamp = Time.time;
                        _state = ArcherAIState.RangedAttack;
                    }

                    break;
                case ArcherAIState.RangedAttack:
                    if (Time.time - _attackTimestamp > attackDuration)
                    {
                        _state = ArcherAIState.Idle;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum ArcherAIState
        {
            Idle,
            RangedAttack
        }
    }
}