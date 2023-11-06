using System;
using Character.Item;
using Scripts.Core.Common;
using Scripts.Core.Global;
using Unity.Mathematics;
using UnityEngine;

namespace Scripts.Character.Classes
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Person : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRend;
        [SerializeField] private bool _isPlayer;
        [SerializeField] [Range(0, 10)] private int _movementSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Loot _loot;
        private TurnHandler _turnHandler;

        [NonSerialized] public UsableItem item;
        private Rigidbody2D _rbody;
        private bool _isGround;

        private void Awake()
        {
            _spriteRend ??= GetComponentInChildren<SpriteRenderer>();
            _rbody = GetComponent<Rigidbody2D>();
            _turnHandler = new TurnHandler();
        }

        #region Character

        public void UseItem()
        {
            if (item != null) item.PrimaryAction();
        }

        private TurnHandler.playerSides GetCurrentSide() => _turnHandler.currentSide;

        public void SetSpriteSide(TurnHandler.playerSides side)
        {
            if (GetCurrentSide() == side) return;

            _turnHandler.SetCharacterSprite(side, _spriteRend.transform);
        }

        private void CheckSpriteSide(Vector2 direction)
        {
            if (direction.x < 0) SetSpriteSide(TurnHandler.playerSides.Left);
            if (direction.x > 0) SetSpriteSide(TurnHandler.playerSides.Right);
        }

        public void MoveTo(Vector2 movementDirection)
        {
            CheckSpriteSide(movementDirection);

            float speed = _movementSpeed * GlobalConstants.CoefMovementSpeed;

            ExecuteCommand(new MoveCommand(_rbody, movementDirection, speed));
        }

        public void Jump()
        {
            if (!_isGround) return;

            ExecuteCommand(new JumpCommand(_rbody,
                    Vector2.up * _jumpForce,
                    ForceMode2D.Impulse));
        }

        public void StopMove()
        {
            ExecuteCommand(new MoveCommand(_rbody, Vector2.zero, 0));
        }

        public void RotateByAngle(Transform obj, float angle)
        {
            ExecuteCommandByValue(new RotationCommand(obj), angle);
        }

        public virtual void Death()
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2);
            Instantiate(_loot, pos, quaternion.identity);
            EventHandler.OnEnemyKilled?.Invoke();
            Destroy(gameObject);

            if (_isPlayer)
                EventHandler.OnPlayerDeath?.Invoke();
        }

        #endregion

        #region Common

        private void ExecuteCommand(Command command) => command.Execute();

        private void ExecuteCommandByValue(Command command, float value) => command.ExecuteByValue(value);

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.transform.CompareTag("Ground")) _isGround = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.CompareTag("Ground")) _isGround = false;
        }

        #endregion
    }
}