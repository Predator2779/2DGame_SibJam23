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
        [SerializeField] private bool _isPlayer;
        [SerializeField] [Range(0, 10)] private int _movementSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Loot _loot;

        private SpriteRenderer _spriteRend;
        private TurnHandler _turnHandler;

        public UsableItem item;
        private Rigidbody2D _rbody;

        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
            _spriteRend = GetComponentInChildren<SpriteRenderer>();
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

        public void MoveTo(Vector2 movementDirection)
        {
            float speed = _movementSpeed * GlobalConstants.CoefMovementSpeed;

            ExecuteCommand(new MoveCommand(_rbody, movementDirection, speed));
        }

        public void Jump()
        {
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

        public void Death()
        {
            if (!_isPlayer)
            {
                Vector2 pos = new Vector2(transform.position.x, -transform.localScale.y / 2);
                Instantiate(_loot, pos, quaternion.identity);
                EventHandler.OnEnemyKilled?.Invoke();
                Destroy(gameObject);
                return;
            }

            EventHandler.OnPlayerDeath?.Invoke();
        }

        #endregion

        #region Common

        private void ExecuteCommand(Command command) => command.Execute();

        private void ExecuteCommandByValue(Command command, float value) => command.ExecuteByValue(value);

        #endregion
    }
}